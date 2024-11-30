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
        public async Task AddCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetMaxCartIdAsync()
        {
            // Lấy giá trị lớn nhất của Id trong bảng Cart
            return await _context.Carts.AnyAsync()
                ? await _context.Carts.MaxAsync(c => c.Id)
                : 0; // Trả về 0 nếu chưa có Cart nào
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                .Include(ci => ci.IdFoodNavigation) // Bao gồm thông tin món ăn
                .Where(ci => ci.IdCart == cartId)
                .ToListAsync();
        }
        public async Task RemoveCartItemAsync(int idCart, int idFood)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.IdCart == idCart && ci.IdFood == idFood);

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
