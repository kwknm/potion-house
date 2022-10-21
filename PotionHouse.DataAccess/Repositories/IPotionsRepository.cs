using PotionHouse.DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IPotionsRepository
    {
        Task<Potion> CreateAsync(Potion potion);
        Task<Potion?> GetByIdAsync(int id);
        Task<List<Potion>> GetManyAsync(int offset = 0, int limit = 15);
        Task<bool> RemoveByIdAsync(int id);
        Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10);
    }
}