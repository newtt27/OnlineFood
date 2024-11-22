using OnlineFood.Models;

namespace OnlineFood.Models.Repositories
{
    public interface IFoodCategoryRepo
    {

        Task<IEnumerable<FoodCategory>> GetAll();
    }
}
