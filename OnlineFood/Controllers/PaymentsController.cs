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
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        public PaymentsController(OnlineFoodContext context, IPaymentService paymentService, ILogger<PaymentsController> logger, IAccountService accountService, ICartService cartService)
        {
            _logger = logger;
            _paymentService = paymentService;
            _context = context;
            _accountService = accountService;
            _cartService = cartService;
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

                return Json(new { success = false, message = "Không thể tạo bill." });
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
        [HttpPost]
        public async Task<IActionResult> CompletePayment()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if ( !userId.HasValue || userId.Value <= 0 )
            {
                return BadRequest("Invalid user ID.");
            }

            Console.WriteLine($"Received userId: {userId}");
            var user = await _context.Accounts
                .Include(u => u.IdCartNavigation) // Bao gồm Cart của người dùng
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }
            Console.WriteLine($"User found: {user.TenHienThi}, CartId: {user.IdCart}");
            // Kiểm tra nếu người dùng có Cart
            var cart = user.IdCartNavigation;
            if (cart != null)
            {
                // Xóa tất cả CartItems liên quan đến Cart này
                var cartItems = await _context.CartItems
                    .Where(ci => ci.IdCart == cart.Id)
                    .ToListAsync();

                if (cartItems.Any())
                {
                    _context.CartItems.RemoveRange(cartItems); // Xóa các CartItems
                }

                // Xóa Cart của người dùng
                _context.Carts.Remove(cart); // Xóa Cart

                // Cập nhật IdCart của người dùng thành null
                user.IdCart = null;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Thực hiện các hành động khác như gửi email, tạo hóa đơn, v.v.
            return Ok("Payment completed and cart cleared.");
        }

        public async Task SaveChangesAsync()
        {
                await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Content))
            {
                return BadRequest("Email và nội dung là bắt buộc.");
            }

            // Log dữ liệu nhận được để kiểm tra
            Console.WriteLine($"Received email: {request.Email}");
            Console.WriteLine($"Received content: {request.Content}");

            // Tiến hành gửi email
            try
            {
                // Giả sử bạn đã cấu hình và sử dụng phương thức gửi email ở đây
                await _paymentService.SendEmail(request.Email, request.Content);
                return Ok("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDisplayName(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("UserId is required.");
            }

            // Truy vấn cơ sở dữ liệu hoặc dịch vụ để lấy thông tin tài khoản
            var account = await _accountService.GetAccountByIdAsync(userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            // Trả về tên hiển thị
            return Ok(new { account });
            //return Ok(new { tenHienThi = account.TenHienThi });
        }
        [HttpGet]
        public IActionResult GetUserId()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                return Ok(new { userId });
            }
            userId = 3;
            return Ok(new { userId });
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
