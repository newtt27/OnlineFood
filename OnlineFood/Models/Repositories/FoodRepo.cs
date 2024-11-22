using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Models.Repositories
{
    public class FoodRepo : IFoodRepo
    {
        private readonly OnlineFoodContext _context;
        public FoodRepo(OnlineFoodContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetById(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
    }
}
