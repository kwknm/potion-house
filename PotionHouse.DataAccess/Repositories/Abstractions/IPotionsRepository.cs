using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Repositories.Abstractions;

public interface IPotionsRepository : IRepository<Potion>
{
    Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10);
}