using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Repositories.Abstractions;

public interface IIngredientsRepository : IRepository<Ingredient>
{
    Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10);
}