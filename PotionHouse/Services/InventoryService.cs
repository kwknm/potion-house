using PotionHouse.DataAccess.Dtos;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<List<IngredientWithAmount>?> GetUserIngredientsAsync(string userId)
    {
        return await _inventoryRepository.GetUserIngredientsAsync(userId);
    }
    
    public async Task<List<PotionWithAmount>?> GetUserPotionsAsync(string userId)
    {
        return await _inventoryRepository.GetUserPotionsAsync(userId);
    }
}