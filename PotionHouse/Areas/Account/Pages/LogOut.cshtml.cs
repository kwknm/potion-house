using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Areas.Account.Pages;

[Authorize]
public class LogOut : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogOut(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPagePermanent("Index");
    }
}