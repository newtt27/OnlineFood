namespace OnlineFood.Models.Services
{
    public interface ICartService
    {
        Task<Cart?> GetCartByAccountIdAsync(int accountId);
        Task<Cart> CreateCartForAccountAsync(Account account);
        Task AddToCartAsync(int accountId, int foodId, int quantity);
        Task<int> GetMaxCartIdAsync();
        Task<IEnumerable<CartItem>> GetCartItemsByAccountIdAsync(int accountId);
        Task RemoveFromCartAsync(int accountId, int foodId);
    }
}
