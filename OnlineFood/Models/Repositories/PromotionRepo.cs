using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Models.Repositories
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly OnlineFoodContext _context;

        public PromotionRepo(OnlineFoodContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int promotionId)
        {
            return await _context.Promotions.FindAsync(promotionId);
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
        {

            var promotions = await _context.Promotions
                .Where(p => p.TrangThai == 1)
                .ToListAsync();


            // Ghi thông tin ra console
            if (promotions.Any())
            {
                foreach (var promo in promotions)
                {
                    Console.WriteLine($"Promotion ID: {promo.Id}, Name: {promo.Ten}, Start: {promo.NgayBd}, End: {promo.NgayKt}, Status: {promo.TrangThai}");
                }
            }
            else
            {
                Console.WriteLine("No active promotions found.");
            }

            return promotions;
        }



        public async Task AddPromotionAsync(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePromotionAsync(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
