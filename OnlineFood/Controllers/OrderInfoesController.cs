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
    public class OrderInfoesController : Controller
    {
        private readonly OnlineFoodContext _context;

        public OrderInfoesController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: OrderInfoes
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.OrderInfos.Include(o => o.IdMonAnNavigation).Include(o => o.IdUserNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: OrderInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfos
                .Include(o => o.IdMonAnNavigation)
                .Include(o => o.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInfo == null)
            {
                return NotFound();
            }

            return View(orderInfo);
        }

        // GET: OrderInfoes/Create
        public IActionResult Create()
        {
            ViewData["IdMonAn"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IdUser"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: OrderInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUser,IdMonAn,Mota,SoLuongHangMon")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMonAn"] = new SelectList(_context.Foods, "Id", "Id", orderInfo.IdMonAn);
            ViewData["IdUser"] = new SelectList(_context.Customers, "Id", "Id", orderInfo.IdUser);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfos.FindAsync(id);
            if (orderInfo == null)
            {
                return NotFound();
            }
            ViewData["IdMonAn"] = new SelectList(_context.Foods, "Id", "Id", orderInfo.IdMonAn);
            ViewData["IdUser"] = new SelectList(_context.Customers, "Id", "Id", orderInfo.IdUser);
            return View(orderInfo);
        }

        // POST: OrderInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUser,IdMonAn,Mota,SoLuongHangMon")] OrderInfo orderInfo)
        {
            if (id != orderInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderInfoExists(orderInfo.Id))
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
            ViewData["IdMonAn"] = new SelectList(_context.Foods, "Id", "Id", orderInfo.IdMonAn);
            ViewData["IdUser"] = new SelectList(_context.Customers, "Id", "Id", orderInfo.IdUser);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfos
                .Include(o => o.IdMonAnNavigation)
                .Include(o => o.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInfo == null)
            {
                return NotFound();
            }

            return View(orderInfo);
        }

        // POST: OrderInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderInfo = await _context.OrderInfos.FindAsync(id);
            if (orderInfo != null)
            {
                _context.OrderInfos.Remove(orderInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderInfoExists(int id)
        {
            return _context.OrderInfos.Any(e => e.Id == id);
        }
    }
}
