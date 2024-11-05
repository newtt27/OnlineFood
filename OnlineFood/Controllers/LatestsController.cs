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
    public class LatestsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public LatestsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Latests
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Latests.Include(l => l.IdBillNavigation).Include(l => l.IdBillSuppliesNavigation).Include(l => l.IdCustomerNavigation).Include(l => l.IdOrderSuppliesNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Latests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latest = await _context.Latests
                .Include(l => l.IdBillNavigation)
                .Include(l => l.IdBillSuppliesNavigation)
                .Include(l => l.IdCustomerNavigation)
                .Include(l => l.IdOrderSuppliesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latest == null)
            {
                return NotFound();
            }

            return View(latest);
        }

        // GET: Latests/Create
        public IActionResult Create()
        {
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id");
            ViewData["IdBillSupplies"] = new SelectList(_context.BillSupplies, "Id", "Id");
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["IdOrderSupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id");
            return View();
        }

        // POST: Latests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdBill,IdBillSupplies,Date,NoiDung,IdCustomer,IdOrderSupplies")] Latest latest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(latest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", latest.IdBill);
            ViewData["IdBillSupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", latest.IdBillSupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", latest.IdCustomer);
            ViewData["IdOrderSupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", latest.IdOrderSupplies);
            return View(latest);
        }

        // GET: Latests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latest = await _context.Latests.FindAsync(id);
            if (latest == null)
            {
                return NotFound();
            }
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", latest.IdBill);
            ViewData["IdBillSupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", latest.IdBillSupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", latest.IdCustomer);
            ViewData["IdOrderSupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", latest.IdOrderSupplies);
            return View(latest);
        }

        // POST: Latests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdBill,IdBillSupplies,Date,NoiDung,IdCustomer,IdOrderSupplies")] Latest latest)
        {
            if (id != latest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(latest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LatestExists(latest.Id))
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
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", latest.IdBill);
            ViewData["IdBillSupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", latest.IdBillSupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", latest.IdCustomer);
            ViewData["IdOrderSupplies"] = new SelectList(_context.OrderSupplies, "Id", "Id", latest.IdOrderSupplies);
            return View(latest);
        }

        // GET: Latests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latest = await _context.Latests
                .Include(l => l.IdBillNavigation)
                .Include(l => l.IdBillSuppliesNavigation)
                .Include(l => l.IdCustomerNavigation)
                .Include(l => l.IdOrderSuppliesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latest == null)
            {
                return NotFound();
            }

            return View(latest);
        }

        // POST: Latests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var latest = await _context.Latests.FindAsync(id);
            if (latest != null)
            {
                _context.Latests.Remove(latest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LatestExists(int id)
        {
            return _context.Latests.Any(e => e.Id == id);
        }
    }
}
