using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public Register(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
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

        var user = new IdentityUser();
        await _userManager.SetEmailAsync(user, Email);
        await _userManager.SetUserNameAsync(user, UserName);

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