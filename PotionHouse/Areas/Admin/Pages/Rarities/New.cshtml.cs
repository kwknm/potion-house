using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Rarities;

[ValidateAntiForgeryToken]
public class New : PageModel
{
    [BindProperty] public string Title { get; set; }
    [BindProperty, Display(Name = "Background Color")] public string BgColor { get; set; }
    [BindProperty, Display(Name = "Text Color")] public string TextColor { get; set; }
    public string? Message { get; set; }

    private readonly IRarityService _rarityService;

    public New(IRarityService rarityService)
    {
        _rarityService = rarityService;
    }

    public void OnGet()
    {
        // todo: add cases
    }

    public async Task OnPostAsync()
    {
        var result = await _rarityService.CreateAsync(Title, BgColor, TextColor);
        Message = result.IsSuccess 
            ? $"Rarity {result.Value.Title} created successfully with Id = {result.Value.Id}" 
            : $"Some error occured: '${result.Errors.First()}'";
    }
}