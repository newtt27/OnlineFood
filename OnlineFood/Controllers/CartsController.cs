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
    public class CartsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public CartsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var onlineFoodContext = _context.Carts.Include(c => c.IdFoodNavigation).Include(c => c.IdKmNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        //public IActionResult LoadCart()
        //{
        //    // Lấy giỏ hàng từ Session (nếu chưa có thì tạo mới)
        //    var cart = HttpContext.Session.GetObjectFromJson<List<Dictionary<string, object>>>("Cart")
        //               ?? new List<Dictionary<string, object>>();

        //    // Đếm số lượng sản phẩm trong giỏ hàng
        //    int cartCount = cart.Sum(item => Convert.ToInt32(item["Quantity"]));

        //    // Trả về Partial View `cart-modal` và số lượng sản phẩm
        //    ViewBag.CartCount = cartCount;
        //    return PartialView("_CartModal", cart);
        //}

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.IdFoodNavigation)
                .Include(c => c.IdKmNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdKm,IdFood")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", cart.IdFood);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", cart.IdKm);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", cart.IdFood);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", cart.IdKm);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdKm,IdFood")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["IdFood"] = new SelectList(_context.Foods, "Id", "Id", cart.IdFood);
            ViewData["IdKm"] = new SelectList(_context.Promotions, "Id", "Id", cart.IdKm);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.IdFoodNavigation)
                .Include(c => c.IdKmNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
