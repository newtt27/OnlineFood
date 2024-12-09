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
    public class FoodsController : Controller
    {
        private readonly OnlineFoodContext _context;

        public FoodsController(OnlineFoodContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()   //int? pageNumber
        {
            //int pageSize = 5; // Số bản ghi trên mỗi trang
            //var foodsQuery = _context.Foods.Include(f => f.IdDanhMucNavigation).AsQueryable();

            //// Tổng số bản ghi
            //int totalCount = await foodsQuery.CountAsync();

            //// Phân trang
            //var foods = await foodsQuery
            //    .OrderBy(f => f.Id) // Sắp xếp
            //    .Skip((pageNumber.GetValueOrDefault(1) - 1) * pageSize) // Bỏ qua
            //    .Take(pageSize) // Lấy số bản ghi cần thiết
            //    .ToListAsync();

            //// Tính tổng số trang
            //int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            //// Truyền dữ liệu sang view
            //ViewData["TotalPages"] = totalPages;
            //ViewData["CurrentPage"] = pageNumber ?? 1;

            //return View(foods);
            var onlineFoodContext = _context.Foods.Include(f => f.IdDanhMucNavigation);
            return View(await onlineFoodContext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.IdDanhMucNavigation)
                .Include(f => f.InfoFoods) // Include bảng InfoFood
                .ThenInclude(i => i.IdNguyenLieuNavigation) // Include bảng Supplies thông qua InfoFood
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["IdDanhMuc"] = new SelectList(_context.FoodCategories, "Id", "Id");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenMonAn,IdDanhMuc,GiaTien,TrangThai,Soluong,Hinhanh,Size,Mota")] Food food, IFormFile HinhanhFile)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid"); // Debug log
                // Xử lý file upload
                if (HinhanhFile != null && HinhanhFile.Length > 0)
                {
                    // Tạo tên file duy nhất để tránh trùng lặp
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhanhFile.FileName);

                    // Đường dẫn để lưu file vào thư mục "wwwroot/images"
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image", fileName);

                    // Lưu file vào đường dẫn
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhanhFile.CopyToAsync(stream);
                    }

                    // Gán đường dẫn hình ảnh vào thuộc tính `Hinhanh` của model
                    food.Hinhanh = "/assets/image/" + fileName;
                }
                _context.Add(food);
                await _context.SaveChangesAsync();

                Console.WriteLine("Data saved successfully!"); // Log kiểm tra
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("ModelState is invalid"); // Debug log
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.FoodCategories, "Id", "Id", food.IdDanhMuc);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.FoodCategories, "Id", "Id", food.IdDanhMuc);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenMonAn,IdDanhMuc,GiaTien,TrangThai,Soluong,Hinhanh,Size,Mota")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            ViewData["IdDanhMuc"] = new SelectList(_context.FoodCategories, "Id", "Id", food.IdDanhMuc);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.IdDanhMucNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
        //chức năng phân trang
        //public async Task<IActionResult> Index(int? pageNumber)
        //{
        //    int pageSize = 5; // Số bản ghi trên mỗi trang
        //    var foods = _context.Foods.AsQueryable();

        //    // Tổng số bản ghi
        //    int totalCount = await foods.CountAsync();

        //    // Phân trang sử dụng Skip và Take
        //    var paginatedFoods = await foods
        //        .OrderBy(f => f.Id) // Sắp xếp theo Id (hoặc cột khác tùy ý)
        //        .Skip((pageNumber.GetValueOrDefault(1) - 1) * pageSize) // Bỏ qua các bản ghi đầu tiên
        //        .Take(pageSize) // Lấy số bản ghi cần thiết
        //        .ToListAsync();

        //    // Tính tổng số trang
        //    int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        //    // Truyền dữ liệu sang view
        //    ViewData["TotalPages"] = totalPages;
        //    ViewData["CurrentPage"] = pageNumber ?? 1;

        //    return View(paginatedFoods);
        //}
    }
}
