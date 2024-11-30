using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;

namespace OnlineFood.Models.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly OnlineFoodContext _context;

        public CartRepo(OnlineFoodContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByAccountIdAsync(int accountId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Accounts.Any(a => a.Id == accountId));
        }

        public async Task<Cart> CreateCartForAccountAsync(Account account)
        {
            var cart = new Cart
            {
                Accounts = new List<Account> { account }
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
