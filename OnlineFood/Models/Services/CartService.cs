using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepository;
        private readonly IFoodService _foodService;
        private readonly IAccountRepo _accountRepository;

        public CartService(ICartRepo cartRepository, IFoodService foodService, IAccountRepo accountRepository)
        {
            _cartRepository = cartRepository;
            _foodService = foodService;
            _accountRepository = accountRepository;
        }

        public async Task<Cart?> GetCartByAccountIdAsync(int accountId)
        {
            return await _cartRepository.GetCartByAccountIdAsync(accountId);
        }

        public async Task<Cart> CreateCartForAccountAsync(Account account)
        {
            return await _cartRepository.CreateCartForAccountAsync(account);
        }

        public async Task AddToCartAsync(int accountId, int foodId, int quantity)
        {
            var cart = await GetCartByAccountIdAsync(accountId);

            if (cart == null)
            {
                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (account == null)
                    throw new Exception("Account not found.");

                cart = await CreateCartForAccountAsync(account);
            }

            var food = await _foodService.GetFoodById(foodId);
            if (food == null)
                throw new Exception("Food not found.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.IdFood == foodId);
            if (cartItem != null)
            {
                cartItem.SoLuong += quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
            else
            {
                var newCartItem = new CartItem
                {
                    IdCart = cart.Id,
                    IdFood = foodId,
                    SoLuong = quantity
                };
                await _cartRepository.AddCartItemAsync(newCartItem);
            }
        }
    }
}
