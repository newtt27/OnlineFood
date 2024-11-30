using OnlineFood.Models;
using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetAllFoods();
        Task<IEnumerable<Food>> GetFoodsByCategory(int categoryId);
        Task<Food> GetFoodById(int id);

    }
}
