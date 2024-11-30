namespace OnlineFood.Models.Repositories
{
    public interface ICartRepo
    {
        Task<Cart?> GetCartByAccountIdAsync(int accountId);
        Task<Cart> CreateCartForAccountAsync(Account account);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task SaveChangesAsync();
        Task AddCartAsync(Cart cart);
        Task<int> GetMaxCartIdAsync();
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);

        Task RemoveCartItemAsync(int idCart, int idFood);
    }
}
