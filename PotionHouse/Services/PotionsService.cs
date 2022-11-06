using FluentResults;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class PotionsService : IPotionsService
{
    private readonly IPotionsRepository _potionsRepository;

    public PotionsService(IPotionsRepository potionsRepository)
    {
        _potionsRepository = potionsRepository;
    }

    public async Task<Potion?> GetByIdAsync(int id)
    {
        return await _potionsRepository.GetByIdAsync(id);
    }

    public async Task<List<Potion>> GetPotionsAsync(int offset = 0, int limit = 15)
    {
        return await _potionsRepository.GetAllAsync(offset, limit);
    }

    public async Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10)
    {
        return await _potionsRepository.SearchByTitleAsync(title, limit);
    }

    public async Task<Potion> CreateAsync(
        string title,
        string description,
        TimeSpan preparationTime,
        decimal preparationCost,
        List<Ingredient> recipe,
        string? image = null)
    {
        var potion = new Potion
        {
            Title = title,
            Description = description,
            PreparationTime = preparationTime,
            Ingredients = recipe,
            PreparationCost = preparationCost,
            Image = image
        };

        var result = _potionsRepository.Add(potion);
        await _potionsRepository.SaveChangesAsync();
        return result;
    }

    public async Task<Result> RemoveByIdAsync(int id)
    {
        var potion = await _potionsRepository.GetByIdAsync(id);
        if (potion is null)
            return Result.Fail("Potion not found");
        _potionsRepository.Remove(potion);
        await _potionsRepository.SaveChangesAsync();
        return Result.Ok();
    }
}
