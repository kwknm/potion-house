using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Pages.Potions;

public class Index : PageModel
{
    public Potion? Potion { get; set; }

    private readonly IPotionsService _potionsService;
    private readonly IInventoryRepository _inventoryRepository;

    public Index(IInventoryRepository inventoryRepository, IPotionsService potionsService)
    {
        _inventoryRepository = inventoryRepository;
        _potionsService = potionsService;
    }

    public async Task OnGetAsync(int id)
    {
        Potion = await _potionsService.GetByIdAsync(id);
    }
}