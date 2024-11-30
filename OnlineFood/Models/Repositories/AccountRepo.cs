using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;

namespace OnlineFood.Models.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly OnlineFoodContext _context;
       

        public AccountRepo(OnlineFoodContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<Account?> GetAccountByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.UserName == username && a.MatKhau == password);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart?> GetCartByAccountIdAsync(int accountId)
        {
            return await _context.Carts
                .Include(c => c.CartItems) // Bao gồm các mục trong giỏ hàng
                .ThenInclude(ci => ci.IdFoodNavigation) // Bao gồm thông tin món ăn
                .FirstOrDefaultAsync(c => c.Accounts.Any(a => a.Id == accountId));
        }
    }
}
