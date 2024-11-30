namespace OnlineFood.Models.Services
{
    public interface IAccountService
    {
        Task<Account?> LoginAsync(string username, string password);
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
    }
}
