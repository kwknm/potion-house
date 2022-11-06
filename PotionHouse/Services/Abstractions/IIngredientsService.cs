using FluentResults;
using PotionHouse.DataAccess.Entities;

namespace PotionHouse.Services.Abstractions
{
    public interface IIngredientsService
    {
        Task<Ingredient> CreateAsync(string title, string description, string? image = null);
        Task<Ingredient?> GetByIdAsync(int id);
        Task<List<Ingredient>> GetAllAsync(int limit = 15, int offset = 0);
        Task<Result> RemoveAsync(int id);
        Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10);
    }
}