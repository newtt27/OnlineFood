using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodId, int quantity)
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Người dùng chưa đăng nhập
                var food = await _foodService.GetFoodById(foodId);
                if (food == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy món ăn" });
                }

                // Tạo dữ liệu để trả về lưu vào localStorage
                var cartItem = new
                {
                    id = food.Id,
                    name = food.TenMonAn,
                    price = food.GiaTien * 1000, // Giá nhân với 1000 nếu cần chuyển đổi đơn vị
                    quantity = quantity,         // Đảm bảo tên thuộc tính là "quantity"
                    image = food.Hinhanh ?? "/assets/default-image.jpg"
                };
                Console.WriteLine("Trả về cartItem cho localStorage:", cartItem);

                return Json(new { success = true, localStorageItem = cartItem, message = "Đã lưu sản phẩm vào localStorage" });
            }
            try
            {
                // Người dùng đã đăng nhập
                await _cartService.AddToCartAsync(userId.Value, foodId, quantity);
                Console.WriteLine("Thêm sản phẩm vào giỏ hàng cho UserId: " + userId);

                return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
