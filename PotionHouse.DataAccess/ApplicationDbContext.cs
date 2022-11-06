using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PotionHouse.DataAccess.Entities;

#nullable disable

namespace PotionHouse.DataAccess;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserPotion>().HasKey(x => new { x.ApplicationUserId, x.PotionId });
        builder.Entity<UserIngredients>().HasKey(x => new { x.ApplicationUserId, x.IngredientId });
        
        builder.Entity<Ingredient>().Navigation(e => e.Rarity).AutoInclude();
        builder.Entity<Potion>().Navigation(e => e.Rarity).AutoInclude();
    }

    public DbSet<Potion> Potions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<UserPotion> UserPotions { get; set; }
    public DbSet<UserIngredients> UserIngredients { get; set; }
    public DbSet<Rarity> Rarities { get; set; }
}
