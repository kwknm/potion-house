using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PotionHouse.Areas.Account.Pages;

[Authorize]
public class LogOut : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LogOut(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPagePermanent("Index");
    }
}