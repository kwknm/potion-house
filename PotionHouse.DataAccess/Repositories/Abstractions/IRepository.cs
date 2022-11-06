using System.Linq.Expressions;
using PotionHouse.DataAccess.Entities.Common;

namespace PotionHouse.DataAccess.Repositories.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    public T Add(T entity);
    public Task<List<T>> GetAllAsync(int limit = 5, int offset = 0);
    public Task<T?> GetByIdAsync(int id);
    public Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    public void Remove(T entity);
    public void RemoveRange(IEnumerable<T> entities);
    public Task SaveChangesAsync();
}