using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services
{
    public interface IPotionsService
    {
        Task<Potion> CreateAsync(
            string title,
            string description,
            TimeSpan preparationTime,
            decimal preparationCost,
            List<Ingredient> recipe,
            string image = null);
        Task<Potion?> GetByIdAsync(int id);
        Task<List<Potion>> GetPotionsAsync(int offset = 0, int limit = 15);
        Task<bool> RemoveAsync(int id);
        Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10);
    }
}