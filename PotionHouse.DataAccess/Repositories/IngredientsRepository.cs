using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess;
using PotionHouse.DataAccess.Entities;

namespace DataAccess.Repositories;

public class IngredientsRepository : IIngredientsRepository
{
	private readonly ApplicationDbContext _context;

	public IngredientsRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Ingredient?> GetByIdAsync(int id)
	{
		return await _context.Ingredients.FindAsync(id);
	}

	public async Task<List<Ingredient>> GetManyAsync(int offset = 0, int limit = 15)
	{
		return await _context.Ingredients.Skip(offset).Take(limit).ToListAsync();
	}

	public async Task<List<Ingredient>> SearchByTitleAsync(string title, int limit = 10)
	{
		return await _context.Ingredients
			.AsNoTracking()
			.Where(x => EF.Functions.Like(x.Title, title)) //todo: add .Trim() whether search does not work correctly with white-spaces
			.Take(limit)
			.ToListAsync();
	}

	public async Task<Ingredient> CreateAsync(Ingredient ingredient)
	{
		var result = await _context.Ingredients.AddAsync(ingredient);
		await _context.SaveChangesAsync();
		return result.Entity;
	}

	public async Task<bool> RemoveByIdAsync(int id)
	{
		var ingredient = await GetByIdAsync(id);
		if (ingredient is null)
			return false;
		_context.Ingredients.Remove(ingredient);
		await _context.SaveChangesAsync();
		return true;
	}
}
