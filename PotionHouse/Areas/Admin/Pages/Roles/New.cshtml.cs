using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Roles;

public class New : PageModel
{
    public string? Message { get; set; }
    [BindProperty, Display(Name = "Role Name")] public string RoleName { get; set; }

    private readonly IRolesService _rolesService;

    public New(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    public void OnGet()
    {
        
    }

    public async Task OnPostAsync()
    {
        var result = await _rolesService.CreateAsync(RoleName);
        Message = result.IsSuccess
            ? $"Role {RoleName} was successfully created"
            : result.ToString();
    }
}