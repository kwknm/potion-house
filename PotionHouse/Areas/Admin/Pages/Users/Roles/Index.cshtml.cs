using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Users.Roles;

public class Index : PageModel
{
    [BindProperty(SupportsGet = true), Display(Name = "User Id")]
    public string? UserId { get; set; }

    public IList<string>? UserRoles { get; set; }

    private readonly IRolesService _rolesService;
    public string? ErrorMessage { get; set; }

    public Index(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    public async Task OnGetAsync()
    {
        if (UserId is not null)
        {
            var result = await _rolesService.GetUserRolesAsync(UserId);
            if (result.IsFailed)
            {
                ErrorMessage = result.Errors.First().Message;
                return;
            }

            UserRoles = result.Value;
        }
    }

    public IActionResult OnPost()
    {
        return RedirectToRoute(new { userId = UserId });
    }
}