using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class RolesService : IRolesService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RolesService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ApplicationRole?> GetByIdAsync(string id)
    {
        return await _roleManager.FindByIdAsync(id);
    }

    public async Task<List<ApplicationRole>> GetAllAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<Result<string>> CreateAsync(string roleName)
    {
        var role = new ApplicationRole();
        await _roleManager.SetRoleNameAsync(role, roleName);
        var result = await _roleManager.CreateAsync(role);

        return result.Succeeded
            ? result.ToString()
            : Result.Fail(result.Errors.First().Description);
    }

    public async Task<Result> RemoveByIdAsync(string id)
    {
        var role = await GetByIdAsync(id);
        if (role is null)
            return Result.Fail("Role not found");

        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.ToString());
    }

    public async Task<Result> AddUserToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Fail("User not found");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.ToString());
    }

    public async Task<Result<IList<string>>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Fail("User not found");

        var roles = await _userManager.GetRolesAsync(user);
        return Result.Ok(roles);
    }

    public async Task<Result> RemoveUserFromRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Fail("User not found");

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.ToString());
    }
}