using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PotionHouse.Areas.Account.Pages;

public class Denied : PageModel
{
    public string? ReturnUrl { get; set; }
    
    public void OnGet(string? returnUrl)
    {
        ReturnUrl = returnUrl;
    }
}