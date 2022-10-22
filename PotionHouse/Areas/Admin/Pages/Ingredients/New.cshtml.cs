using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.Services;

namespace PotionHouse.Areas.Admin.Pages.Ingredients;

//[Authorize(Policy = "admin")]
public class New : PageModel
{
    [BindProperty] public string Title { get; set; }
    [BindProperty] public string Description { get; set; }
    [BindProperty] public IFormFile Image { get; set; }

    private readonly IIngredientsService _ingredientsService;
    private readonly IFilesService _filesService;

    public New(IIngredientsService ingredientsService, IFilesService filesService)
    {
        _ingredientsService = ingredientsService;
        _filesService = filesService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var path = await _filesService.UploadImageAsync(Image);

        if (path is null)
        {
            ModelState.AddModelError(nameof(Image), "Image is too big");
            return Page();
        }

        var ingredient = await _ingredientsService.CreateAsync(Title, Description, path);

        return Page();
    }
}