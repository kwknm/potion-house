using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Dtos;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.Pages.Inventory;

[Authorize]
public class Index : PageModel
{
    public List<PotionWithAmount>? PotionsWithAmounts { get; set; }
    public List<IngredientWithAmount>? IngredientsWithAmounts { get; set; }

    private readonly IInventoryRepository _inventoryRepository;

    public Index(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        IngredientsWithAmounts = await _inventoryRepository.GetUserIngredientsAsync(userId);
        PotionsWithAmounts = await _inventoryRepository.GetUserPotionsAsync(userId);
    }
}