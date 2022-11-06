using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Pages.Ingredients;

public class List : PageModel
{
    public List<Ingredient> Ingredients { get; set; }

    private readonly IIngredientsService _ingredientsService;

    public List(IIngredientsService ingredientsService)
    {
        _ingredientsService = ingredientsService;
    }

    public async Task OnGetAsync()
    {
        Ingredients = await _ingredientsService.GetAllAsync(limit: 50);
    }
}