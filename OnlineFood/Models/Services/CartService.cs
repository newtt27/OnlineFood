using OnlineFood.Models;
using OnlineFood.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Models.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo; // Đã thêm CartRepo
        private readonly IFoodService _foodService;
        private readonly IAccountService _accountService;
        private readonly IPromotionService _promotionService;

        public CartService(
            ICartRepo cartRepo,
            IFoodService foodService,
            IAccountService accountService,
            IPromotionService promotionService)
        {
            _cartRepo = cartRepo;
            _foodService = foodService;
            _accountService = accountService;
            _promotionService = promotionService;
        }

        public async Task<Cart?> GetCartByAccountIdAsync(int accountId)
        {
            return await _cartRepo.GetCartByAccountIdAsync(accountId);
        }

        public async Task<Cart> CreateCartForAccountAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account không được null.");

            // Lấy danh sách các Promotion còn hiệu lực
            var activePromotions = await _promotionService.GetActivePromotionsAsync();
            if (!activePromotions.Any())
                throw new Exception("Không có Promotion nào còn hiệu lực.");

            // Chọn một Promotion ngẫu nhiên
            var random = new Random();
            var randomPromotion = activePromotions.ElementAt(random.Next(activePromotions.Count()));

            int maxCartId = await _cartRepo.GetMaxCartIdAsync();
            var newCartId = maxCartId + 1;
            // Tạo mới Cart
            var newCart = new Cart
            {
                Id = newCartId,
                IdKm = randomPromotion.Id // Gán Promotion ngẫu nhiên
            };

            // Thêm Cart vào cơ sở dữ liệu
            await _cartRepo.AddCartAsync(newCart);

            // Cập nhật IdCart cho Account
            account.IdCart = newCart.Id;
            await _accountService.UpdateAccountAsync(account);

            return newCart;
        }

        public async Task AddToCartAsync(int accountId, int foodId, int quantity)
        {
            var cart = await GetCartByAccountIdAsync(accountId);

            if (cart == null)
            {
                var account = await _accountService.GetAccountByIdAsync(accountId);
                if (account == null)
                    throw new Exception("Account không tồn tại.");

                cart = await CreateCartForAccountAsync(account);
            }

            var food = await _foodService.GetFoodById(foodId);
            if (food == null)
                throw new Exception("Không tìm thấy món ăn.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.IdFood == foodId);
            if (cartItem != null)
            {
                cartItem.SoLuong += quantity;
                await _cartRepo.UpdateCartItemAsync(cartItem);
            }
            else
            {
                var newCartItem = new CartItem
                {
                    IdCart = cart.Id,
                    IdFood = foodId,
                    SoLuong = quantity
                };
                await _cartRepo.AddCartItemAsync(newCartItem);
            }
        }

        public async Task<int> GetMaxCartIdAsync()
        {
            return await _cartRepo.GetMaxCartIdAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByAccountIdAsync(int accountId)
        {
            // Lấy Cart từ Account
            var cart = await _cartRepo.GetCartByAccountIdAsync(accountId);

            if (cart == null)
            {
                throw new Exception("Giỏ hàng không tồn tại.");
            }

            // Lấy danh sách CartItems từ Cart
            return await _cartRepo.GetCartItemsByCartIdAsync(cart.Id);
        }

        public async Task RemoveFromCartAsync(int accountId, int foodId)
        {
                var account = await _accountService.GetAccountByIdAsync(accountId);

                await _cartRepo.RemoveCartItemAsync(account.IdCart.Value, foodId);
        }

    }
}
