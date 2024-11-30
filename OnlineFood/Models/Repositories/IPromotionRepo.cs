namespace OnlineFood.Models.Repositories
{
    public interface IPromotionRepo
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> GetPromotionByIdAsync(int promotionId);
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync(); // Chỉ các promotion còn hiệu lực
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int promotionId);

    }
}
