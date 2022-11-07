using System.Linq.Expressions;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services.Abstractions;

public interface IUsersService
{
    Task<ApplicationUser?> FindByConditionAsync(Expression<Func<ApplicationUser, bool>> predicate,
        int limit = 15, int offset = 0);

    Task<List<ApplicationUser>> FindByConditionManyAsync(Expression<Func<ApplicationUser, bool>> predicate,
        int limit = 15, int offset = 0);

    Task<ApplicationUser?> FindByIdAsync(string id);
}