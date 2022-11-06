using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class IngredientsRepository : Repository<Ingredient>, IIngredientsRepository
{
    public IngredientsRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10)
    {
        return await _context.Ingredients
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.Title,
                title)) //todo: add .Trim() whether search does not work correctly with white-spaces
            .Take(limit)
            .ToListAsync();
    }
}