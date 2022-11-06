using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities.Common;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class Repository<T> : IRepository<T> 
    where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public T Add(T entity)
    {
        var a = _context.Set<T>().Add(entity);
        return a.Entity;
    }

    public async Task<List<T>> GetAllAsync(int limit = 15, int offset = 0)
    {
        return await _context.Set<T>().AsNoTracking().Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    
    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}