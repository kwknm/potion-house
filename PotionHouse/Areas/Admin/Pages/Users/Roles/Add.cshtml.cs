using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Areas.Admin.Pages.Users.Roles;

public class Add : PageModel
{
    [BindProperty, Display(Name = "User Id")] 
    public string UserId { get; set; }
    [BindProperty, Display(Name = "Role Name")] public string RoleName { get; set; }

    public string? Message { get; set; }
    public List<SelectListItem> Roles { get; set; }

    private readonly IRolesService _rolesService;

    public Add(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    public async Task OnGetAsync()
    {
        await FetchRolesAndMap();
    }

    public async Task OnPostAsync()
    {
        var result = await _rolesService.AddUserToRoleAsync(UserId, RoleName);
        Message = result.IsSuccess
            ? $"Role {RoleName} was successfully added to User with Id = {UserId}"
            : result.Errors.First().Message;
        await FetchRolesAndMap();
    }

    public async Task FetchRolesAndMap()
    {
        Roles = (await _rolesService.GetAllAsync()).Select(x => new SelectListItem(x.Name, x.Name)).ToList();
    }
}