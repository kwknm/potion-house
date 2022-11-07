using FluentResults;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services.Abstractions;

public interface IRolesService
{
    Task<ApplicationRole?> GetByIdAsync(string id);
    Task<List<ApplicationRole>> GetAllAsync();
    Task<Result<string>> CreateAsync(string roleName);
    Task<Result> RemoveByIdAsync(string id);
    Task<Result> AddUserToRoleAsync(string userId, string roleId);
    Task<Result<IList<string>>> GetUserRolesAsync(string userId);
    Task<Result> RemoveUserFromRoleAsync(string userId, string roleName);
}