using OnlineFood.Models;
using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepo _foodRepo;
        public FoodService(IFoodRepo foodRepo)
        {
            _foodRepo = foodRepo;
        }

        public async Task<IEnumerable<Food>> GetAllFoods()
        {
            return await _foodRepo.GetAll();
        }

        public async Task<Food> GetFoodById(int id)
        {
            return await _foodRepo.GetById(id);
        }
    }
}
