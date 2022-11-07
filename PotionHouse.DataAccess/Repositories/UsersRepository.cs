using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ApplicationUser>> FindByConditionManyAsync(Expression<Func<ApplicationUser, bool>> predicate, int limit = 15, int offset = 0)
    {
        return await _context.Users
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .Where(predicate)
            .ToListAsync();
    }
    
    public async Task<ApplicationUser?> FindByConditionAsync(Expression<Func<ApplicationUser, bool>> predicate, int limit = 15, int offset = 0)
    {
        return await _context.Users.AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .FirstOrDefaultAsync(predicate);
    }
}