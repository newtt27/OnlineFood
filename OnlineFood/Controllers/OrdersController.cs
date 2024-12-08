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
    public class OrdersController : Controller
    {
        private readonly OnlineFoodContext _context;

        public OrdersController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Orders.Include(o => o.IdCartNavigation).Include(o => o.IdDeliveryNavigation).Include(o => o.IdFoodNavigation).Include(o => o.IdKhachHangNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.IdCartNavigation)
                .Include(o => o.IdDeliveryNavigation)
                .Include(o => o.IdFoodNavigation)
                .Include(o => o.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["IdCart"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["IdDelivery"] = new SelectList(_context.DeliveryMethods, "Id", "Id");
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdKhachHang,Date,IdFood,TrangThai,TongSoLuong,IdCart,IdDelivery")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCart"] = new SelectList(_context.Carts, "Id", "Id", order.IdCart);
            ViewData["IdDelivery"] = new SelectList(_context.DeliveryMethods, "Id", "Id", order.IdDelivery);
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", order.IdFood);
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", order.IdKhachHang);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Orders
                        .Include(o => o.IdFoodNavigation) // Include related Food details
                        .ThenInclude(f => f.Orders) // Include related orders if needed
                        .FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["IdCart"] = new SelectList(_context.Carts, "Id", "Id", order.IdCart);
            ViewData["IdDelivery"] = new SelectList(_context.DeliveryMethods, "Id", "Id", order.IdDelivery);
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", order.IdFood);
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", order.IdKhachHang);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdKhachHang,Date,IdFood,TrangThai,TongSoLuong,IdCart,IdDelivery")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["IdCart"] = new SelectList(_context.Carts, "Id", "Id", order.IdCart);
            ViewData["IdDelivery"] = new SelectList(_context.DeliveryMethods, "Id", "Id", order.IdDelivery);
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", order.IdFood);
            ViewData["IdKhachHang"] = new SelectList(_context.Customers, "Id", "Id", order.IdKhachHang);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.IdCartNavigation)
                .Include(o => o.IdDeliveryNavigation)
                .Include(o => o.IdFoodNavigation)
                .Include(o => o.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
