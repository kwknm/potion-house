using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Dtos;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Pages.Inventory;

[Authorize]
public class Index : PageModel
{
    public List<PotionWithAmount>? PotionsWithAmounts { get; set; }
    public List<IngredientWithAmount>? IngredientsWithAmounts { get; set; }

    private readonly IInventoryService _inventoryService;

    public Index(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        IngredientsWithAmounts = await _inventoryService.GetUserIngredientsAsync(userId);
        PotionsWithAmounts = await _inventoryService.GetUserPotionsAsync(userId);
    }
}