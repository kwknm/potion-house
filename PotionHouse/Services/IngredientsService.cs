using DataAccess.Repositories;
using PotionHouse.DataAccess.Entities;

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

    public async Task<List<Ingredient>> GetPotionsAsync(int offset = 0, int limit = 15)
    {
        return await _ingredientsRepository.GetManyAsync(offset, limit);
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

        var result = await _ingredientsRepository.CreateAsync(ingredient);
        return result;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        return await _ingredientsRepository.RemoveByIdAsync(id);
    }
}
