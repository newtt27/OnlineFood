namespace OnlineFood.Models.Repositories
{
    public interface IAccountRepo
    {
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task<Account?> GetAccountByUsernameAndPasswordAsync(string username, string password);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task<Cart> GetCartByAccountIdAsync(int accountId);
    }
}