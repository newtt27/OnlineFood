using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Models;
using OnlineFood.Models.Services;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net;

namespace OnlineFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFoodService _foodService;
        private IFoodCategoryService _foodCategoryService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IFoodCategoryService foodCategoryService, IFoodService foodService, ICartService cartService)
        {
            _logger = logger;
            _foodCategoryService = foodCategoryService; 
            _foodService = foodService;
            _cartService = cartService;
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

        [HttpGet]
        public async Task<IActionResult> GetFoodsByCategory(int categoryId)
        {
           
            var foods = await _foodService.GetFoodsByCategory(categoryId);

            if (foods == null || !foods.Any())
            {
                foods = new List<Food>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }

            ViewData["Foods"] = foods;
            // Trả dữ liệu vào partial view
            return PartialView("_FoodCategoryPartial", foods);
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
