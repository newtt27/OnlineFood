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

        public async Task<IEnumerable<Food>> GetAllFoodActive()
        {
            return await _context.Foods
                .Where(f => f.TrangThai == 1)
                .ToListAsync();
        }

        public async Task<Food> GetById(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
        // Lấy danh sách món ăn theo danh mục
        public async Task<IEnumerable<Food>> GetFoodsByCategoryId(int categoryId)
        {
            return await _context.Foods
                           .Where(f => f.IdDanhMuc == categoryId && f.TrangThai == 1) // CategoryId phải trùng với cột trong DB
                           .ToListAsync();
        }

        public async Task<List<Food>> SearchFoodsAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return new List<Food>();
            }

            return await _context.Foods
                                 .Where(f => f.TenMonAn.Contains(keyword) && f.TrangThai == 1)
                                 .ToListAsync(); // sử dụng ToListAsync() thay vì ToList()
        }
        public async Task AddFoodAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }

    }
}
