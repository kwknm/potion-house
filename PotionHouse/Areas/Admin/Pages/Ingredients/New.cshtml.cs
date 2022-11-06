using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Ingredients;


//[Authorize(Policy = "admin")]
[ValidateAntiForgeryToken]
public class New : PageModel
{
    [BindProperty] public string Title { get; set; }
    [BindProperty] public string Description { get; set; }
    [BindProperty] public IFormFile Image { get; set; }
    [BindProperty, Display(Name = "Rarity")] public string RarityId { get; set; }
    public List<SelectListItem> Rarities { get; set; }

    private readonly IIngredientsService _ingredientsService;
    private readonly IFilesService _filesService;
    private readonly IRarityService _rarityService;

    public New(IIngredientsService ingredientsService, IFilesService filesService, IRarityService rarityService)
    {
        _ingredientsService = ingredientsService;
        _filesService = filesService;
        _rarityService = rarityService;
    }

    public async Task OnGetAsync()
    {
        Rarities = (await _rarityService.GetAllAsync(30)).Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!int.TryParse(RarityId, out var rarityId))
        {
            ModelState.AddModelError(nameof(RarityId), "Rarity Id is not a number");
            return Page();
        }
        
        var path = await _filesService.UploadImageAsync(Image);

        if (path is null)
        {
            ModelState.AddModelError(nameof(Image), "Image is too big");
            return Page();
        }

        var ingredient = await _ingredientsService.CreateAsync(Title, Description, path);
        await _rarityService.SetIngredientRarityAsync(ingredient.Id, rarityId);
        return Page();
    }
}