using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class PotionsRepository : Repository<Potion>, IPotionsRepository
{
    public PotionsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10)
    {
        return await _context.Potions
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.Title,
                title)) //todo: add .Trim() whether search does not work correctly with white-spaces
            .Take(limit)
            .ToListAsync();
    }
}