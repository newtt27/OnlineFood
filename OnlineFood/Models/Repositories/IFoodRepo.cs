using OnlineFood.Models;

namespace OnlineFood.Models.Repositories
{
    public interface IFoodRepo
    {

        Task<IEnumerable<Food>> GetAll();
        Task<IEnumerable<Food>> GetFoodsByCategoryId(int categoryId);
        Task<Food> GetById(int id);
    }
}
