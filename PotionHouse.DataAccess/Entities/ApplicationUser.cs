using Microsoft.AspNetCore.Identity;

namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserPotion> Potions { get; set; }
    public ICollection<UserIngredients> Ingredients { get; set; }
}