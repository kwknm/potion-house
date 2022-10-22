namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class Inventory
{
    public ICollection<Potion> Potions { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}