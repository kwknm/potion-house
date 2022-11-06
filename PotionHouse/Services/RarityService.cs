using FluentResults;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class RarityService : IRarityService
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IPotionsRepository _potionsRepository;
    private readonly IRarityRepository _rarityRepository;

    public RarityService(IIngredientsRepository ingredientsRepository, IPotionsRepository potionsRepository,
        IRarityRepository rarityRepository)
    {
        _ingredientsRepository = ingredientsRepository;
        _potionsRepository = potionsRepository;
        _rarityRepository = rarityRepository;
    }

    public async Task<Result> SetIngredientRarityAsync(int ingredientId, int rarityId)
    {
        var rarity = await _rarityRepository.GetByIdAsync(rarityId);
        if (rarity is null)
            return Result.Fail("Rarity not found");
        var ingredient = await _ingredientsRepository.GetByIdAsync(ingredientId);
        if (ingredient is null)
            return Result.Fail("Ingredient not found");
        ingredient.Rarity = rarity;
        await _rarityRepository.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> SetPotionRarityAsync(int potionId, int rarityId)
    {
        var rarity = await _rarityRepository.GetByIdAsync(rarityId);
        if (rarity is null)
            return Result.Fail("Rarity not found");
        var potion = await _potionsRepository.GetByIdAsync(potionId);
        if (potion is null)
            return Result.Fail("Potion not found");
        potion.Rarity = rarity;
        await _rarityRepository.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result<Rarity>> GetRarityByIdAsync(int id)
    {
        var rarity = await _rarityRepository.GetByIdAsync(id);
        if (rarity is null)
            return Result.Fail("Rarity not found");
        return rarity;
    }
    
    public async Task<Result<Rarity>> CreateAsync(string title, string bgColorHex, string textColorHex)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Fail("Title is empty");
        
        if (string.IsNullOrWhiteSpace(bgColorHex))
            return Result.Fail("Bg Color is missing");
        
        if (string.IsNullOrWhiteSpace(textColorHex))
            return Result.Fail("Text Color is empty");
        
        var rarity = new Rarity
        {
            Title = title,
            BgColorHex = bgColorHex,
            TextColorHex = textColorHex
        };
        
        var result = _rarityRepository.Add(rarity);
        await _rarityRepository.SaveChangesAsync();
        return result;
    }

    public async Task<Result> RemoveByIdAsync(int id)
    {
        var rarity = await _rarityRepository.GetByIdAsync(id);
        if (rarity is null)
            return Result.Fail("Rarity not found");
        
        _rarityRepository.Remove(rarity);
        await _rarityRepository.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<List<Rarity>> GetAllAsync(int limit = 5, int offset = 0)
    {
        return await _rarityRepository.GetAllAsync();
    }
}