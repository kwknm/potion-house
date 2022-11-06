using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class RarityRepository : Repository<Rarity>, IRarityRepository
{
    public RarityRepository(ApplicationDbContext context) : base(context)
    {
    }
}