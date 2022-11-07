using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Dtos;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Users;

public class Details : PageModel
{
    public ApplicationUser? User { get; set; }
    public IList<string>? Roles { get; set; }
    public List<IngredientWithAmount>? UserIngredients { get; set; }
    public List<PotionWithAmount>? UserPotions { get; set; }

    private readonly IRolesService _rolesService;
    private readonly IUsersService _usersService;
    private readonly IInventoryService _inventoryService;

    public Details(IRolesService rolesService, IUsersService usersService, IInventoryService inventoryService)
    {
        _rolesService = rolesService;
        _usersService = usersService;
        _inventoryService = inventoryService;
    }

    public async Task OnGetAsync(string id)
    {
        var user = await _usersService.FindByIdAsync(id);
        var roles = await _rolesService.GetUserRolesAsync(id);
        var userIngredients = await _inventoryService.GetUserIngredientsAsync(id);
        var userPotions = await _inventoryService.GetUserPotionsAsync(id);

        if (user is null)
            return;
        UserIngredients = userIngredients;
        UserPotions = userPotions;
        Roles = roles.Value;
        User = user;
    }
}