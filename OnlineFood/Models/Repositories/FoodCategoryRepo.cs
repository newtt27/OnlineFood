
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Models.Repositories
{
    public class FoodCategoryRepo : IFoodCategoryRepo
    {
        private readonly OnlineFoodContext _context;
        public FoodCategoryRepo(OnlineFoodContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FoodCategory>> GetAll()
        {
            return await _context.FoodCategories.ToListAsync();
        }
    }
}
