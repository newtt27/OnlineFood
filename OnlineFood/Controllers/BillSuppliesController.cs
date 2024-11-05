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
    public class BillSuppliesController : Controller
    {
        private readonly OnlineFoodContext _context;

        public BillSuppliesController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: BillSupplies
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.BillSupplies.Include(b => b.IdNhaCungCapNavigation).Include(b => b.IdordersuppliesNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: BillSupplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billSupply = await _context.BillSupplies
                .Include(b => b.IdNhaCungCapNavigation)
                .Include(b => b.IdordersuppliesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billSupply == null)
            {
                return NotFound();
            }

            return View(billSupply);
        }

        // GET: BillSupplies/Create
        public IActionResult Create()
        {
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id");
            ViewData["Idordersupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id");
            return View();
        }

        // POST: BillSupplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idordersupplies,Date,Mota,IdNhaCungCap")] BillSupply billSupply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billSupply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", billSupply.IdNhaCungCap);
            ViewData["Idordersupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", billSupply.Idordersupplies);
            return View(billSupply);
        }

        // GET: BillSupplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billSupply = await _context.BillSupplies.FindAsync(id);
            if (billSupply == null)
            {
                return NotFound();
            }
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", billSupply.IdNhaCungCap);
            ViewData["Idordersupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", billSupply.Idordersupplies);
            return View(billSupply);
        }

        // POST: BillSupplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idordersupplies,Date,Mota,IdNhaCungCap")] BillSupply billSupply)
        {
            if (id != billSupply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billSupply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillSupplyExists(billSupply.Id))
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
            ViewData["IdNhaCungCap"] = new SelectList(_context.Suppliers, "Id", "Id", billSupply.IdNhaCungCap);
            ViewData["Idordersupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", billSupply.Idordersupplies);
            return View(billSupply);
        }

        // GET: BillSupplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billSupply = await _context.BillSupplies
                .Include(b => b.IdNhaCungCapNavigation)
                .Include(b => b.IdordersuppliesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billSupply == null)
            {
                return NotFound();
            }

            return View(billSupply);
        }

        // POST: BillSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billSupply = await _context.BillSupplies.FindAsync(id);
            if (billSupply != null)
            {
                _context.BillSupplies.Remove(billSupply);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillSupplyExists(int id)
        {
            return _context.BillSupplies.Any(e => e.Id == id);
        }
    }
}
