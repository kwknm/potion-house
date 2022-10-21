using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess;
using PotionHouse.DataAccess.Entities;

namespace DataAccess.Repositories;

public class PotionsRepository : IPotionsRepository
{
	private readonly ApplicationDbContext _context;

	public PotionsRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Potion?> GetByIdAsync(int id)
	{
		return await _context.Potions.FindAsync(id);
	}

	public async Task<List<Potion>> GetManyAsync(int offset = 0, int limit = 15)
	{
		return await _context.Potions.Skip(offset).Take(limit).ToListAsync();
	}

	public async Task<List<Potion>> SearchByTitleAsync(string title, int limit = 10)
	{
		return await _context.Potions
			.AsNoTracking()
			.Where(x => EF.Functions.Like(x.Title, title)) //todo: add .Trim() whether search does not work correctly with white-spaces
			.Take(limit)
			.ToListAsync();
	}

	public async Task<Potion> CreateAsync(Potion potion)
	{
		var result = await _context.Potions.AddAsync(potion);
		await _context.SaveChangesAsync();
		return result.Entity;
	}

	public async Task<bool> RemoveByIdAsync(int id)
	{
		var potion = await GetByIdAsync(id);
		if (potion is null)
			return false;
		_context.Potions.Remove(potion);
		await _context.SaveChangesAsync();
		return true;
	}
}
