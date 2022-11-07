using PotionHouse.DataAccess.Dtos;

namespace PotionHouse.Services.Abstractions;

public interface IInventoryService
{
    Task<List<IngredientWithAmount>?> GetUserIngredientsAsync(string userId);
    Task<List<PotionWithAmount>?> GetUserPotionsAsync(string userId);
}