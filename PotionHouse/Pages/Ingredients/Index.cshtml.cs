using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services;

namespace PotionHouse.Pages.Ingredients;

public class Index : PageModel
{
    public Ingredient? Ingredient { get; set; }

    private readonly IIngredientsService _ingredientsService;

    public Index(IIngredientsService ingredientsService)
    {
        _ingredientsService = ingredientsService;
    }

    public async Task OnGetAsync(int id)
    {
        Ingredient = await _ingredientsService.GetByIdAsync(id);
    }
}