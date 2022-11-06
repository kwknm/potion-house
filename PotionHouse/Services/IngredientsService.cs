using FluentResults;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;
using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class IngredientsService : IIngredientsService
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public IngredientsService(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
    {
        return await _ingredientsRepository.GetByIdAsync(id);
    }

    public async Task<List<Ingredient>> GetAllAsync(int limit = 15, int offset = 0)
    {
        return await _ingredientsRepository.GetAllAsync(limit, offset);
    }

    public async Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10)
    {
        return await _ingredientsRepository.SearchByTitleAsync(title, limit);
    }

    public async Task<Ingredient> CreateAsync(
        string title,
        string description,
        string? image = null)
    {
        var ingredient = new Ingredient
        {
            Title = title,
            Description = description,
            Image = image
        };

        var result = _ingredientsRepository.Add(ingredient);
        await _ingredientsRepository.SaveChangesAsync();
        return result;
    }

    public async Task<Result> RemoveAsync(int id)
    {
        var ingredient = await _ingredientsRepository.GetByIdAsync(id);
        if (ingredient is null)
            return Result.Fail("Ingredient not found");
        _ingredientsRepository.Remove(ingredient);
        await _ingredientsRepository.SaveChangesAsync();
        return Result.Ok();
    }
}
