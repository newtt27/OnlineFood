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
    public class CartsController : Controller
    {
        private readonly OnlineFoodContext _context;
        private readonly IFoodService _foodService;
        private readonly IFoodCategoryService _foodCategoryService;
        private readonly ICartService _cartService;
        private readonly IAccountService _accountService;
        public CartsController(OnlineFoodContext context, IFoodCategoryService foodCategoryService, IFoodService foodService, ICartService cartService, IAccountService accountService)
        {
            _context = context;
            _cartService = cartService;
            _foodService = foodService;
            _foodCategoryService = foodCategoryService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult CheckSession()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                return Json(new { loggedIn = true, userId = userId.Value });
            }
            return Json(new { loggedIn = false });
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodId, int quantity)
        {
            // Lấy thông tin người dùng từ Session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Người dùng chưa đăng nhập
                var food = await _foodService.GetFoodById(foodId);
                if (food == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy món ăn" });
                }

                // Trả dữ liệu để lưu vào localStorage
                var cartItem = new
                {
                    id = food.Id,
                    name = food.TenMonAn,
                    price = food.GiaTien * 1000, // Giá nhân với 1000 nếu cần chuyển đổi đơn vị
                    quantity = quantity,
                    image = food.Hinhanh ?? "/assets/default-image.jpg"
                };

                return Json(new { success = true, localStorageItem = cartItem, message = "Đã lưu sản phẩm vào localStorage" });
            }

            try
            {
                var account = await _accountService.GetAccountByIdAsync(userId.Value);

                if (account.IdCart == null)
                {
                    var newCart = await _cartService.CreateCartForAccountAsync(account);
                }
                await _cartService.AddToCartAsync(userId.Value, foodId, quantity);
                return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return Json(new { success = false, message = "Người dùng chưa đăng nhập" });
                }

                // Lấy danh sách CartItems
                var cartItems = await _cartService.GetCartItemsByAccountIdAsync(userId.Value);

                // Trả về JSON dữ liệu
                var result = cartItems.Select(item => new
                {
                    id = item.IdFood,
                    name = item.IdFoodNavigation.TenMonAn,
                    price = item.IdFoodNavigation.GiaTien * 1000,
                    quantity = item.SoLuong,
                    image = item.IdFoodNavigation.Hinhanh ?? "/assets/default-image.jpg"
                });

                return Json(new { success = true, items = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int foodId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thực hiện chức năng này." });
            }

            if (foodId <= 0)
            {
                return Json(new { success = false, message = "Id sản phẩm không hợp lệ." });
            }

            try
            {
                await _cartService.RemoveFromCartAsync(userId.Value, foodId);
                return Json(new { success = true, message = "Sản phẩm đã được xóa khỏi giỏ hàng." });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Lỗi null: {ex.Message}");
                return Json(new { success = false, message = $"Lỗi null: {ex.ParamName}" });
            }
            
        }


    }
}
