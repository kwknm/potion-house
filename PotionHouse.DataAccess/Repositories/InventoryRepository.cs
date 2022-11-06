using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Dtos;
using PotionHouse.DataAccess.Entities;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PotionWithAmount>?> GetUserPotionsAsync(string userId)
    {
        var userPotions = await _context.UserPotions
            .Where(x => x.ApplicationUserId == userId)
            .Select(x => new PotionWithAmount(x.Potion, x.Amount))
            .ToListAsync();
        return userPotions;
    }

    public async Task<List<IngredientWithAmount>?> GetUserIngredientsAsync(string userId)
    {
        var userIngredients = await _context.UserIngredients
            .Where(x => x.ApplicationUserId == userId)
            .Select(x => new IngredientWithAmount(x.Ingredient, x.Amount))
            .ToListAsync();
        return userIngredients;
    }

    public async Task<bool> AddIngredientAsync(string userId, int ingredientId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return false;
        var ingredient = await _context.Ingredients.FindAsync(ingredientId);
        if (ingredient is null)
            return false;

        var userIngredient = _context.UserIngredients.Where(x => x.ApplicationUserId == userId)
            .FirstOrDefault(x => x.IngredientId == ingredientId);

        if (userIngredient is null)
        {
            _context.UserIngredients.Add(new UserIngredients
                { IngredientId = ingredientId, ApplicationUserId = userId, Amount = 1 });
            await _context.SaveChangesAsync();
            return false;
        }

        userIngredient.Amount++;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPotionAsync(string userId, int potionId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return false;
        var potion = await _context.Potions.FindAsync(potionId);
        if (potion is null)
            return false;

        var userPotion = _context.UserPotions.Where(x => x.ApplicationUserId == userId)
            .FirstOrDefault(x => x.PotionId == potionId);

        if (userPotion is null)
        {
            _context.UserPotions.Add(new UserPotion { PotionId = potionId, ApplicationUserId = userId, Amount = 1 });
            await _context.SaveChangesAsync();
            return false;
        }

        userPotion.Amount++;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveIngredientAsync(string userId, int ingredientId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return false;
        var ingredient = await _context.Ingredients.FindAsync(ingredientId);
        if (ingredient is null)
            return false;

        var userIngredient = _context.UserIngredients.Where(x => x.ApplicationUserId == userId)
            .FirstOrDefault(x => x.IngredientId == ingredientId);

        if (userIngredient is null)
            return false;

        if (userIngredient.Amount - 1 < 0)
            return false;

        if (userIngredient.Amount - 1 == 0)
        {
            _context.UserIngredients.Remove(userIngredient);
        }

        userIngredient.Amount--;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemovePotionAsync(string userId, int potionId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return false;
        var potion = await _context.Ingredients.FindAsync(potionId);
        if (potion is null)
            return false;

        var userPotion = _context.UserPotions.Where(x => x.ApplicationUserId == userId)
            .FirstOrDefault(x => x.PotionId == potionId);

        if (userPotion is null)
            return false;

        if (userPotion.Amount - 1 <= 0)
            return false;

        if (userPotion.Amount - 1 == 0)
        {
            _context.UserPotions.Remove(userPotion);
        }

        userPotion.Amount--;

        await _context.SaveChangesAsync();

        return true;
    }
}