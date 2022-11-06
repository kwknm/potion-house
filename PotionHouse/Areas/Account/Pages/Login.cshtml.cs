using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Areas.Account.Pages;

public class LoginModel : PageModel
{
    [BindProperty, EmailAddress] public string Email { get; set; }
    [BindProperty, DataType(DataType.Password)] public string Password { get; set; }

    [BindProperty, DisplayName("Remember me?")]
    public bool RememberMe { get; set; } = true;
    

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.FindByEmailAsync(Email);
        if (user is null)
        {
            ModelState.AddModelError(nameof(Email), "Incorrect Email or Password");
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user, Password, RememberMe, false);
        if (result.Succeeded) return RedirectPermanent(returnUrl ?? "/");
        
        ModelState.AddModelError(nameof(Email), "Incorrect Email or Password");
        return Page();

    }
}