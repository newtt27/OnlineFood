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
    public class OrderSuppliesController : Controller
    {
        private readonly OnlineFoodContext _context;

        public OrderSuppliesController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: OrderSupplies
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.OrderSupplies.Include(o => o.IdNhaCungCapNavigation).Include(o => o.IdNhanVienNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: OrderSupplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSupply = await _context.OrderSupplies
                .Include(o => o.IdNhaCungCapNavigation)
                .Include(o => o.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderSupply == null)
            {
                return NotFound();
            }

            return View(orderSupply);
        }

        // GET: OrderSupplies/Create
        public IActionResult Create()
        {
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id");
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: OrderSupplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SoLuongNguyenLieu,Mota,IdNhanVien,IdNhaCungCap,Datetime,TrangThai,TenNguyenLieu,Donvi")] OrderSupply orderSupply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderSupply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", orderSupply.IdNhaCungCap);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", orderSupply.IdNhanVien);
            return View(orderSupply);
        }

        // GET: OrderSupplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSupply = await _context.OrderSupplies.FindAsync(id);
            if (orderSupply == null)
            {
                return NotFound();
            }
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", orderSupply.IdNhaCungCap);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", orderSupply.IdNhanVien);
            return View(orderSupply);
        }

        // POST: OrderSupplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SoLuongNguyenLieu,Mota,IdNhanVien,IdNhaCungCap,Datetime,TrangThai,TenNguyenLieu,Donvi")] OrderSupply orderSupply)
        {
            if (id != orderSupply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderSupply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderSupplyExists(orderSupply.Id))
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
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", orderSupply.IdNhaCungCap);
            ViewData["IdNhanVien"] = new SelectList(_context.Employees, "Id", "Id", orderSupply.IdNhanVien);
            return View(orderSupply);
        }

        // GET: OrderSupplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSupply = await _context.OrderSupplies
                .Include(o => o.IdNhaCungCapNavigation)
                .Include(o => o.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderSupply == null)
            {
                return NotFound();
            }

            return View(orderSupply);
        }

        // POST: OrderSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderSupply = await _context.OrderSupplies.FindAsync(id);
            if (orderSupply != null)
            {
                _context.OrderSupplies.Remove(orderSupply);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderSupplyExists(int id)
        {
            return _context.OrderSupplies.Any(e => e.Id == id);
        }
    }
}
