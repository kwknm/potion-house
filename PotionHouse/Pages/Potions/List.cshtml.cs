using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Pages.Potions;

public class ListModel : PageModel
{
    [BindProperty]
    public List<Potion> Potions { get; set; }

    private readonly IPotionsService _potionsService;
    private readonly IIngredientsService _ingredientsService;

    public ListModel(IPotionsService potionsService, IIngredientsService ingredientsService)
    {
        _potionsService = potionsService;
        _ingredientsService = ingredientsService;
    }

    public async Task OnGet()
    {
        // todo: get a list of potions (and recipes for them)
        var potions = await _potionsService.GetPotionsAsync();
        Potions = potions;
    }

    public async Task OnPost()
    {
        var ing = await _ingredientsService.GetByIdAsync(1);

        var potion = await _potionsService.CreateAsync(
            title: "Relic Struct",
            description: "Some struct",
            preparationTime: TimeSpan.FromMinutes(3),
            preparationCost: 100M,
            recipe: new List<Ingredient>() { ing, ing });
    }
}
