using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineFood.Data;
using OnlineFood.Models;
using OnlineFood.Models.Services;

namespace OnlineFood.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly OnlineFoodContext _context;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(OnlineFoodContext context, IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
            _context = context;
        }
        public IActionResult Card()
        {
            // Lấy userId từ session và gán vào ViewBag
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.PaymentMethod = "Card";
            ViewBag.Instructions = "Please enter your card details.";
            return View();
        }

        public IActionResult Momo()
        {
            ViewData["PaymentMethod"] = "Momo";
            ViewData["Instructions"] = "Use the Momo app to complete your payment.";
            return View();
        }

        public IActionResult Zalopay()
        {
            ViewData["PaymentMethod"] = "Momo";
            ViewData["Instructions"] = "Use the Zalopay app to complete your payment.";
            return View();
        }
        // GET: Payments
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }
        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhuongThucThanhToan,Mota,TrangThai")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
        //PROCESS PAYMENT
        [HttpPost]
        public async Task<ActionResult> ProcessPayment([FromBody] PaymentData paymentData)
        {
            try
            {
                if (paymentData == null)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }

                string mota = "Người dùng thanh toán ";

                // Tạo đối tượng Payment từ paymentData
                var payment = new Payment
                {
                    Id = await _paymentService.GetMaxPaymentIdAsync() + 1, // Sử dụng await để lấy ID mới
                    PhuongThucThanhToan = paymentData.paymentData.PhuongThucThanhToan,
                    Mota = mota + paymentData.paymentData.Mota,
                    TrangThai = "Chờ xác nhận", // Đã thanh toán qua merchant hay chưa
                    
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync(); // Lưu Payment vào DB bất đồng bộ

                // Tạo Bill liên quan đến Payment
                var createBillResult = await CreateBill(payment.Id, paymentData.tongTienTruoc, paymentData.tongTienSau, paymentData.userId);
                // Kiểm tra kết quả trả về từ CreateBill
                // Kiểm tra nếu createBillResult là JsonResult
                if (createBillResult is JsonResult jsonResult && jsonResult.Value is not null)
                {
                    var result = jsonResult.Value as dynamic;
                    if (result?.success == true)
                    {
                        return Json(new { success = true, message = "Payment and Bill created successfully." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to create bill." });
                    }
                }

                return Json(new { success = true, message = "Payment created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing payment: {ex.Message} | Inner: {ex.InnerException?.Message}");
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng thử lại." });
            }
        }

        // CREATE BILL
        [HttpPost]
        public async Task<ActionResult> CreateBill(int paymentId, double tongTienTruoc, double tongTienSau, int userId, int idNhanVien = 1, int idKm = 1, int idOrderInfo = 1)
        {
            try
            {
                var bill = new Bill
                {
                    Id = paymentId,
                    NgayCheckIn = DateTime.Now,
                    NgayCheckOut = DateTime.Now.AddHours(1),
                    TrangThai = 0, // Ví dụ trạng thái
                    TongTienTruoc = tongTienTruoc,
                    TongTienSau = tongTienSau,
                    IdPhuongThuc = paymentId,
                    IdKhachHang = userId,
                    IdNhanVien = idNhanVien,
                    IdKm = idKm,
                    IdOrderInfo = idOrderInfo,
                };

                _context.Bills.Add(bill);
                await _context.SaveChangesAsync(); // Lưu Bill vào DB bất đồng bộ
                _logger.LogInformation($"Bill created successfully with Id: {bill.Id}");

                return Json(new { success = true, message = "Bill created successfully.", billId = bill.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bill creation failed. PaymentId: {paymentId}, Subtotal: {tongTienTruoc}, Total: {tongTienSau}, UserId: {userId}");
                _logger.LogError($"Error creating bill: {ex.Message}");
                return Json(new { success = false, message = "Lỗi khi tạo hóa đơn." });
            }
        }
        public async Task SaveChangesAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                _logger.LogInformation($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database update error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Hoặc xử lý lỗi tùy trường hợp
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }
        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhuongThucThanhToan,Mota,TrangThai")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
