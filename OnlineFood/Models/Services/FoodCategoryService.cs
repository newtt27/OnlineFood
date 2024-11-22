using OnlineFood.Models;
using OnlineFood.Models.Repositories;

namespace OnlineFood.Models.Services
{
    public class FoodCategoryService : IFoodCategoryService
    {
        private readonly IFoodCategoryRepo _foodCategoryRepo;
        public FoodCategoryService(IFoodCategoryRepo foodCategoryRepo)
        {
            _foodCategoryRepo = foodCategoryRepo;
        }

        public async Task<IEnumerable<FoodCategory>> GetAll()
        {
            return await _foodCategoryRepo.GetAll();
        }
    }
}
