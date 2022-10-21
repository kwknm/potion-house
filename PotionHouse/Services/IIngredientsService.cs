using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services
{
    public interface IIngredientsService
    {
        Task<Ingredient> CreateAsync(string title, string description, string? image = null);
        Task<Ingredient?> GetByIdAsync(int id);
        Task<List<Ingredient>> GetPotionsAsync(int offset = 0, int limit = 15);
        Task<bool> RemoveAsync(int id);
        Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10);
    }
}