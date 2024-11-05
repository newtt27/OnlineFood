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
    public class InfoFoodsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public InfoFoodsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: InfoFoods
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.InfoFoods.Include(i => i.IdFoodNavigation).Include(i => i.IdNguyenLieuNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: InfoFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoFood = await _context.InfoFoods
                .Include(i => i.IdFoodNavigation)
                .Include(i => i.IdNguyenLieuNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoFood == null)
            {
                return NotFound();
            }

            return View(infoFood);
        }

        // GET: InfoFoods/Create
        public IActionResult Create()
        {
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IdNguyenLieu"] = new SelectList(_context.Supplies, "Id", "Id");
            return View();
        }

        // POST: InfoFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Songuyenlieucan,IdFood,Mota,IdNguyenLieu")] InfoFood infoFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infoFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", infoFood.IdFood);
            ViewData["IdNguyenLieu"] = new SelectList(_context.Supplies, "Id", "Id", infoFood.IdNguyenLieu);
            return View(infoFood);
        }

        // GET: InfoFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoFood = await _context.InfoFoods.FindAsync(id);
            if (infoFood == null)
            {
                return NotFound();
            }
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", infoFood.IdFood);
            ViewData["IdNguyenLieu"] = new SelectList(_context.Supplies, "Id", "Id", infoFood.IdNguyenLieu);
            return View(infoFood);
        }

        // POST: InfoFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Songuyenlieucan,IdFood,Mota,IdNguyenLieu")] InfoFood infoFood)
        {
            if (id != infoFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoFoodExists(infoFood.Id))
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
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", infoFood.IdFood);
            ViewData["IdNguyenLieu"] = new SelectList(_context.Supplies, "Id", "Id", infoFood.IdNguyenLieu);
            return View(infoFood);
        }

        // GET: InfoFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infoFood = await _context.InfoFoods
                .Include(i => i.IdFoodNavigation)
                .Include(i => i.IdNguyenLieuNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoFood == null)
            {
                return NotFound();
            }

            return View(infoFood);
        }

        // POST: InfoFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var infoFood = await _context.InfoFoods.FindAsync(id);
            if (infoFood != null)
            {
                _context.InfoFoods.Remove(infoFood);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoFoodExists(int id)
        {
            return _context.InfoFoods.Any(e => e.Id == id);
        }
    }
}
