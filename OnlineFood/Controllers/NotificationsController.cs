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
    public class NotificationsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public NotificationsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Notifications
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Notifications.Include(n => n.IdBillNavigation).Include(n => n.IdBillsuppliesNavigation).Include(n => n.IdCustomerNavigation).Include(n => n.IdOrderNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .Include(n => n.IdBillNavigation)
                .Include(n => n.IdBillsuppliesNavigation)
                .Include(n => n.IdCustomerNavigation)
                .Include(n => n.IdOrderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // GET: Notifications/Create
        public IActionResult Create()
        {
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id");
            ViewData["IdBillsupplies"] = new SelectList(_context.BillSupplies, "Id", "Id");
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["IdOrder"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCustomer,IdOrder,Name,NoiDung,Date,IdBill,IdBillsupplies")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", notification.IdBill);
            ViewData["IdBillsupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", notification.IdBillsupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", notification.IdCustomer);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "Id", "Id", notification.IdOrder);
            return View(notification);
        }

        // GET: Notifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", notification.IdBill);
            ViewData["IdBillsupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", notification.IdBillsupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", notification.IdCustomer);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "Id", "Id", notification.IdOrder);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCustomer,IdOrder,Name,NoiDung,Date,IdBill,IdBillsupplies")] Notification notification)
        {
            if (id != notification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationExists(notification.Id))
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
            ViewData["IdBill"] = new SelectList(_context.Bills, "Id", "Id", notification.IdBill);
            ViewData["IdBillsupplies"] = new SelectList(_context.BillSupplies, "Id", "Id", notification.IdBillsupplies);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Id", notification.IdCustomer);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "Id", "Id", notification.IdOrder);
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .Include(n => n.IdBillNavigation)
                .Include(n => n.IdBillsuppliesNavigation)
                .Include(n => n.IdCustomerNavigation)
                .Include(n => n.IdOrderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
