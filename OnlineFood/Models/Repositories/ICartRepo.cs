namespace OnlineFood.Models.Repositories
{
    public interface ICartRepo
    {
        Task<Cart?> GetCartByAccountIdAsync(int accountId);
        Task<Cart> CreateCartForAccountAsync(Account account);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task SaveChangesAsync();
    }
}
