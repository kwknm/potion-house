using FluentResults;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services.Abstractions;

public interface IRarityService
{
    Task<Result> SetIngredientRarityAsync(int ingredientId, int rarityId);
    Task<Result> SetPotionRarityAsync(int potionId, int rarityId);
    Task<Result<Rarity>> GetRarityByIdAsync(int id);
    Task<Result<Rarity>> CreateAsync(string title, string bgColorHex, string textColorHex);
    Task<Result> RemoveByIdAsync(int id);
    Task<List<Rarity>> GetAllAsync(int limit = 5, int offset = 0);
}