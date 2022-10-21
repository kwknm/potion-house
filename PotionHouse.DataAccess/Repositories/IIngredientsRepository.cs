using PotionHouse.DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IIngredientsRepository
    {
        Task<Ingredient> CreateAsync(Ingredient ingredient);
        Task<Ingredient?> GetByIdAsync(int id);
        Task<List<Ingredient>> GetManyAsync(int offset = 0, int limit = 15);
        Task<bool> RemoveByIdAsync(int id);
        Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10);
    }
}