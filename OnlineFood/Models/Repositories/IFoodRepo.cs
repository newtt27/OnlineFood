using OnlineFood.Models;

namespace OnlineFood.Models.Repositories
{
    public interface IFoodRepo
    {

        Task<IEnumerable<Food>> GetAll();

        Task<Food> GetById(int id);
    }
}
