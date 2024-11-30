using Microsoft.AspNetCore.Mvc;
using OnlineFood.Models;
using OnlineFood.Models.Repositories;
using System.Net;

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

        [HttpGet]
        public async Task<IEnumerable<Food>> GetFoodsByCategory(int categoryId)
        {
            return await _foodRepo.GetFoodsByCategoryId(categoryId);
        }

    }
}
