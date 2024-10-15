using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    public class PaymentdetailsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public PaymentdetailsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Paymentdetails
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Paymentdetails.Include(p => p.IdKhachhangNavigation).Include(p => p.IdKuyenmaiNavigation).Include(p => p.IdPaymentNavigation).Include(p => p.IdSanphamNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Paymentdetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentdetail = await _context.Paymentdetails
                .Include(p => p.IdKhachhangNavigation)
                .Include(p => p.IdKuyenmaiNavigation)
                .Include(p => p.IdPaymentNavigation)
                .Include(p => p.IdSanphamNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentdetail == null)
            {
                return NotFound();
            }

            return View(paymentdetail);
        }

        // GET: Paymentdetails/Create
        public IActionResult Create()
        {
            ViewData["IdKhachhang"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["IdKuyenmai"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["IdPayment"] = new SelectList(_context.Payments, "Id", "Id");
            ViewData["IdSanpham"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Paymentdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdKhachhang,IdSanpham,Soluong,GiatienTruoc,HotenKhachhang,Date,PaymentMode,GiaTienSau,IdKuyenmai,IdPayment")] Paymentdetail paymentdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachhang"] = new SelectList(_context.Customers, "Id", "Id", paymentdetail.IdKhachhang);
            ViewData["IdKuyenmai"] = new SelectList(_context.Events, "Id", "Id", paymentdetail.IdKuyenmai);
            ViewData["IdPayment"] = new SelectList(_context.Payments, "Id", "Id", paymentdetail.IdPayment);
            ViewData["IdSanpham"] = new SelectList(_context.Products, "Id", "Id", paymentdetail.IdSanpham);
            return View(paymentdetail);
        }

        // GET: Paymentdetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentdetail = await _context.Paymentdetails.FindAsync(id);
            if (paymentdetail == null)
            {
                return NotFound();
            }
            ViewData["IdKhachhang"] = new SelectList(_context.Customers, "Id", "Id", paymentdetail.IdKhachhang);
            ViewData["IdKuyenmai"] = new SelectList(_context.Events, "Id", "Id", paymentdetail.IdKuyenmai);
            ViewData["IdPayment"] = new SelectList(_context.Payments, "Id", "Id", paymentdetail.IdPayment);
            ViewData["IdSanpham"] = new SelectList(_context.Products, "Id", "Id", paymentdetail.IdSanpham);
            return View(paymentdetail);
        }

        // POST: Paymentdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdKhachhang,IdSanpham,Soluong,GiatienTruoc,HotenKhachhang,Date,PaymentMode,GiaTienSau,IdKuyenmai,IdPayment")] Paymentdetail paymentdetail)
        {
            if (id != paymentdetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentdetailExists(paymentdetail.Id))
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
            ViewData["IdKhachhang"] = new SelectList(_context.Customers, "Id", "Id", paymentdetail.IdKhachhang);
            ViewData["IdKuyenmai"] = new SelectList(_context.Events, "Id", "Id", paymentdetail.IdKuyenmai);
            ViewData["IdPayment"] = new SelectList(_context.Payments, "Id", "Id", paymentdetail.IdPayment);
            ViewData["IdSanpham"] = new SelectList(_context.Products, "Id", "Id", paymentdetail.IdSanpham);
            return View(paymentdetail);
        }

        // GET: Paymentdetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentdetail = await _context.Paymentdetails
                .Include(p => p.IdKhachhangNavigation)
                .Include(p => p.IdKuyenmaiNavigation)
                .Include(p => p.IdPaymentNavigation)
                .Include(p => p.IdSanphamNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentdetail == null)
            {
                return NotFound();
            }

            return View(paymentdetail);
        }

        // POST: Paymentdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentdetail = await _context.Paymentdetails.FindAsync(id);
            if (paymentdetail != null)
            {
                _context.Paymentdetails.Remove(paymentdetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentdetailExists(int id)
        {
            return _context.Paymentdetails.Any(e => e.Id == id);
        }
    }
}
