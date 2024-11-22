using Microsoft.AspNetCore.Mvc;
using OnlineFood.Models;
using OnlineFood.Models.Services;
using System.Diagnostics;
using System.Drawing.Printing;

namespace OnlineFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFoodService _foodService;
        private IFoodCategoryService _foodCategoryService;
        public HomeController(ILogger<HomeController> logger, IFoodCategoryService foodCategoryService, IFoodService foodService)
        {
            _logger = logger;
            _foodCategoryService = foodCategoryService; 
            _foodService = foodService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var foodCategories = await _foodCategoryService.GetAll();
            var foods = await _foodService.GetAllFoods();

            var totalItems = foods.Count();

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Lấy danh sách món ăn theo trang hiện tại (Skip và Take)
            var paginatedFoods = foods.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["Foods"] = paginatedFoods;
            ViewData["FoodCategories"] = foodCategories;
            ViewData["CurrentPage"] = page;          // Trang hiện tại
            ViewData["TotalPages"] = totalPages;     // Tổng số trang

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetPagedFoods(int page = 1, int pageSize = 8)
        {
            var foods = await _foodService.GetAllFoods() ?? new List<Food>(); // Đảm bảo foods không null
            var totalItems = foods.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Lấy danh sách món ăn cho trang hiện tại
            var paginatedFoods = foods.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["Foods"] = paginatedFoods; // Gán vào ViewData
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return PartialView("_FoodListPartial", paginatedFoods);
        }


        [HttpGet]
        public async Task<IActionResult> GetFoodDetails(int id)
        {
            var food = await _foodService.GetFoodById(id); 

            if (food == null)
            {
                return NotFound();
            }
            else
            {
                return PartialView("_FoodDetails", food);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodId, int quantity)
        {
            var food = await _foodService.GetFoodById(foodId);
            if (food == null)
            {
                return Json(new { success = false, message = "Không tìm thấy món ăn" });
            }

            var cartItem = new
            {
                id = food.Id,
                name = food.TenMonAn,
                price = food.GiaTien * 1000,
                quantity = quantity,
                image = food.Hinhanh
            };

            return Json(new { success = true, item = cartItem });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
