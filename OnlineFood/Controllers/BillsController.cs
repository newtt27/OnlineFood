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
    public class BillsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public BillsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Bills.Include(b => b.IdKhachHangNavigation).Include(b => b.IdKmNavigation).Include(b => b.IdNhanVienNavigation).Include(b => b.IdOrderInfoNavigation).Include(b => b.IdPhuongThucNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.IdKhachHangNavigation)
                .Include(b => b.IdKmNavigation)
                .Include(b => b.IdNhanVienNavigation)
                .Include(b => b.IdOrderInfoNavigation)
                .Include(b => b.IdPhuongThucNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id");
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["IdOrderInfo"] = new SelectList(_context.OrderInfos, "Id", "Id");
            ViewData["IdPhuongThuc"] = new SelectList(_context.Payments, "Id", "Id");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgayCheckIn,NgayCheckOut,TrangThai,IdKhachHang,IdNhanVien,IdKm,TongTienTruoc,TongTienSau,IdPhuongThuc,IdOrderInfo")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", bill.IdKhachHang);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", bill.IdKm);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", bill.IdNhanVien);
            ViewData["IdOrderInfo"] = new SelectList(_context.OrderInfos, "Id", "Id", bill.IdOrderInfo);
            ViewData["IdPhuongThuc"] = new SelectList(_context.Payments, "Id", "Id", bill.IdPhuongThuc);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", bill.IdKhachHang);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", bill.IdKm);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", bill.IdNhanVien);
            ViewData["IdOrderInfo"] = new SelectList(_context.OrderInfos, "Id", "Id", bill.IdOrderInfo);
            ViewData["IdPhuongThuc"] = new SelectList(_context.Payments, "Id", "Id", bill.IdPhuongThuc);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayCheckIn,NgayCheckOut,TrangThai,IdKhachHang,IdNhanVien,IdKm,TongTienTruoc,TongTienSau,IdPhuongThuc,IdOrderInfo")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", bill.IdKhachHang);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", bill.IdKm);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", bill.IdNhanVien);
            ViewData["IdOrderInfo"] = new SelectList(_context.OrderInfos, "Id", "Id", bill.IdOrderInfo);
            ViewData["IdPhuongThuc"] = new SelectList(_context.Payments, "Id", "Id", bill.IdPhuongThuc);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.IdKhachHangNavigation)
                .Include(b => b.IdKmNavigation)
                .Include(b => b.IdNhanVienNavigation)
                .Include(b => b.IdOrderInfoNavigation)
                .Include(b => b.IdPhuongThucNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
