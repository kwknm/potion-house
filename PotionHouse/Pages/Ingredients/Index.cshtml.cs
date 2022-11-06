using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Pages.Ingredients;

public class Index : PageModel
{
    public Ingredient? Ingredient { get; set; }

    private readonly IIngredientsService _ingredientsService;
    private readonly IInventoryRepository _inventoryRepository;

    public Index(IIngredientsService ingredientsService, IInventoryRepository inventoryRepository)
    {
        _ingredientsService = ingredientsService;
        _inventoryRepository = inventoryRepository;
    }

    public async Task OnGetAsync(int id)
    {
        Ingredient = await _ingredientsService.GetByIdAsync(id);
        if (Ingredient is not null)
        {
            var r = await _inventoryRepository.AddIngredientAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier), this.Ingredient.Id);
        }
    }
}