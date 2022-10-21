using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PotionHouse.Areas.Account.Pages;

public class RegisterModel : PageModel
{
    [BindProperty, Required, StringLength(16, MinimumLength = 3)]
    public string UserName { get; set; }
    [BindProperty, Required, EmailAddress]
    public string Email { get; set; }
    [BindProperty, Required]
    public string Password { get; set; }

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        var candidate = _userManager.FindByNameAsync(UserName);
        if (candidate is not null)
        {
            ModelState.AddModelError(nameof(UserName), "Username is already in use");
            return Page();
        }

        var user = new IdentityUser();
        await _userManager.SetUserNameAsync(user, UserName);
        await _userManager.SetEmailAsync(user, Email);

        var result = await _userManager.CreateAsync(user, Password);
        if (!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await _signInManager.SignInAsync(user, true);
        return RedirectToPage("/");
    }
}
