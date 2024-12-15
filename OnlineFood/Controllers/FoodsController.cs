using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;
using OnlineFood.Models.Services;

namespace OnlineFood.Controllers
{
    public class FoodsController : Controller
    {
        private readonly OnlineFoodContext _context;
        private readonly IFoodService _foodService;
        private readonly ILogger<FoodsController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FoodsController(OnlineFoodContext context,IFoodService foodService, ILogger<FoodsController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _foodService = foodService;
            _hostingEnvironment = hostingEnvironment;
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
            var danhMucList = _context.FoodCategories.Select(dm => new SelectListItem
            {
                Value = dm.Id.ToString(),
                Text = dm.TenDanhMuc
            }).ToList();
            ViewBag.IdDanhMuc = danhMucList;
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
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(HinhanhFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("HinhanhFile", "Only image files (.jpg, .jpeg, .png, .gif) are allowed.");
                    }
                    else if (HinhanhFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("HinhanhFile", "File size must not exceed 5 MB.");
                    }
                    else
                    {
                        // Generate a unique file name
                        //var fileName = Guid.NewGuid().ToString() + extension;
                        var webrootPath = _hostingEnvironment.WebRootPath;
                        var fileName = DateTime.UtcNow.ToString("yymmssfff") + Path.GetFileNameWithoutExtension(HinhanhFile.FileName) + Path.GetExtension(HinhanhFile.FileName);
                        // Save file to wwwroot/assets/image
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "image");
                        var uploadDir = @"assets/image";
                        var filePath = Path.Combine(webrootPath, uploadDir, fileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhanhFile.CopyToAsync(stream);
                        }

                        // Save file path to the model
                        food.Hinhanh = "/assets/image/" + fileName;
                        food.Id=GenerateNewFoodId();
                    }
                    var foodCategory = await _context.FoodCategories.FindAsync(food.IdDanhMuc);
                    if (foodCategory == null)
                    {
                        ModelState.AddModelError("IdDanhMuc", "Invalid category selected.");
                    }
                    else
                    {
                        food.IdDanhMucNavigation = foodCategory;
                        _context.Foods.Add(food);
                        await _context.SaveChangesAsync();
                        
                        return RedirectToAction(nameof(Index));
                    }
                }
                var danhMucList = _context.FoodCategories.Select(dm => new SelectListItem
                {
                    Value = dm.Id.ToString(),
                    Text = dm.TenDanhMuc
                }).ToList();
                ViewBag.IdDanhMuc = danhMucList;  
                
            }
            else
            {
                Console.WriteLine("ModelState is invalid"); // Debug log
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            //ViewData["IdDanhMuc"] = new SelectList(_context.FoodCategories, "Id", "Id", food.IdDanhMuc);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        // kiểm tra id trong account
        private int GenerateNewFoodId()
        {
            var maxId = _context.Foods.Max(a => (int?)a.Id) ?? 0;

            // Tăng Id lên 1
            return maxId + 1;
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
