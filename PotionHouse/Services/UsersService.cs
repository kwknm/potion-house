using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUsersRepository _usersRepository;

    public UsersService(UserManager<ApplicationUser> userManager, IUsersRepository usersRepository)
    {
        _userManager = userManager;
        _usersRepository = usersRepository;
    }

    public async Task<List<ApplicationUser>> FindByConditionManyAsync(Expression<Func<ApplicationUser, bool>> predicate,
        int limit = 15, int offset = 0)
    {
        return await _usersRepository.FindByConditionManyAsync(predicate, limit, offset);
    }
    
    public async Task<ApplicationUser?> FindByConditionAsync(Expression<Func<ApplicationUser, bool>> predicate,
        int limit = 15, int offset = 0)
    {
        return await _usersRepository.FindByConditionAsync(predicate, limit, offset);
    }

    public async Task<ApplicationUser?> FindByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}