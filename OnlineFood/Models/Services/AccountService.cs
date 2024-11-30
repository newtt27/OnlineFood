using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepository;

        public AccountService(IAccountRepo accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account?> LoginAsync(string username, string password)
        {
            return await _accountRepository.GetAccountByUsernameAndPasswordAsync(username, password);
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _accountRepository.GetAccountByIdAsync(accountId);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _accountRepository.AddAccountAsync(account);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.DeleteAccountAsync(accountId);
        }

        public async Task<Cart> GetCartByAccountIdAsync(int accountId)
        {
            return await _accountRepository.GetCartByAccountIdAsync(accountId);
        }
    }
}
