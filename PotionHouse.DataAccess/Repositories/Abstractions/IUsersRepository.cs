using System.Linq.Expressions;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Repositories.Abstractions;

public interface IUsersRepository
{
    Task<ApplicationUser?> FindByConditionAsync(Expression<Func<ApplicationUser, bool>> predicate, int limit = 15,
        int offset = 0);

    Task<List<ApplicationUser>> FindByConditionManyAsync(Expression<Func<ApplicationUser, bool>> predicate,
        int limit = 15, int offset = 0);
}