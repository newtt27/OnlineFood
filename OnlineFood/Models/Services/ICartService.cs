namespace OnlineFood.Models.Services
{
    public interface ICartService
    {
        Task<Cart?> GetCartByAccountIdAsync(int accountId);
        Task<Cart> CreateCartForAccountAsync(Account account);
        Task AddToCartAsync(int accountId, int foodId, int quantity);
    }
}
