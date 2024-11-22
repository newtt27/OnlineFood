using OnlineFood.Models;
using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetAllFoods();

        Task<Food> GetFoodById(int id);

    }
}
