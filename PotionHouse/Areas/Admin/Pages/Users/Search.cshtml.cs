using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Users;

public class Search : PageModel
{
    public Search(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [BindProperty(SupportsGet = true), Display(Name = "Username")]
    public string? UserName { get; set; }

    public List<ApplicationUser>? Users { get; set; }

    private readonly IUsersService _usersService;

    public async Task OnGetAsync()
    {
        if (UserName is not null)
        {
            Users = await _usersService.FindByConditionManyAsync(x =>
                Regex.IsMatch(x.NormalizedUserName, UserName.Trim().ToUpper()));
        }
    }

    public IActionResult OnPost()
    {
        return RedirectToRoute(new { username = UserName });
    }
}