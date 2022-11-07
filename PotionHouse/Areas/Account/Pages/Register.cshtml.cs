using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Areas.Account.Pages;

#nullable disable

public class Register : PageModel
{
    [BindProperty, DisplayName("Username"), StringLength(16, MinimumLength = 3)]
    public string UserName { get; set; }
    
    [BindProperty, DisplayName("Email"), EmailAddress] 
    public string Email { get; set; }

    [BindProperty, DataType(DataType.Password), DisplayName("Password")]
    public string Password { get; set; }

    [BindProperty, DataType(DataType.Password), Compare(nameof(Password)), DisplayName("Confirm password")]
    public string ConfirmPassword { get; set; }

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public Register(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult OnGet()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToPagePermanent("Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var candidate = await _userManager.FindByNameAsync(UserName);
        if (candidate is not null)
        {
            ModelState.AddModelError(nameof(UserName), "Username is already in use");
            return Page();
        }

        var user = new ApplicationUser();
        await _userManager.SetEmailAsync(user, Email);
        await _userManager.SetUserNameAsync(user, UserName);
        await _userManager.SetLockoutEnabledAsync(user, false);

        var result = await _userManager.CreateAsync(user, Password);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(nameof(UserName), result.Errors.First().Description);
            return Page();
        }

        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(user, true);
        
        return RedirectToPagePermanent("Index");
    }
}