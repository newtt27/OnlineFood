using OnlineFood.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineFood.Models.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepo _promotionRepository;

        public PromotionService(IPromotionRepo promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            return await _promotionRepository.GetAllPromotionsAsync();
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int promotionId)
        {
            return await _promotionRepository.GetPromotionByIdAsync(promotionId);
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
        {
            return await _promotionRepository.GetActivePromotionsAsync();
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            await _promotionRepository.AddPromotionAsync(promotion);
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            await _promotionRepository.UpdatePromotionAsync(promotion);
        }

        public async Task DeletePromotionAsync(int promotionId)
        {
            await _promotionRepository.DeletePromotionAsync(promotionId);
        }
    }
}
