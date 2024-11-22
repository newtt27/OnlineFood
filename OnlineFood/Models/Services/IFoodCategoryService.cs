using OnlineFood.Models;

namespace OnlineFood.Models.Services
{
    public interface IFoodCategoryService
    {
        Task<IEnumerable<FoodCategory>> GetAll();
    }
}
