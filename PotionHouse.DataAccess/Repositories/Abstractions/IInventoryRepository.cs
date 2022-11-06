using PotionHouse.DataAccess.Dtos;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Repositories.Abstractions;

public interface IInventoryRepository
{
    Task<List<PotionWithAmount>?> GetUserPotionsAsync(string userId);
    Task<List<IngredientWithAmount>?> GetUserIngredientsAsync(string userId);
    Task<bool> AddIngredientAsync(string userId, int ingredientId);
    Task<bool> AddPotionAsync(string userId, int potionId);
    Task<bool> RemoveIngredientAsync(string userId, int ingredientId);
    Task<bool> RemovePotionAsync(string userId, int potionId);
}