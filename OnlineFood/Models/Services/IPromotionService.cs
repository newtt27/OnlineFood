namespace OnlineFood.Models.Services
{
    public interface IPromotionService
    {
        Task<Promotion?> GetPromotionByIdAsync(int promotionId);
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync(); // Chỉ các promotion còn hiệu lực
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int promotionId);
    }
}
