using PotionHouse.DataAccess.Entities.Common;

namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class Potion : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TimeSpan PreparationTime { get; set; }
    public decimal PreparationCost { get; set; }
    public string Image { get; set; }
    public Rarity Rarity { get; set; }
    
    public ICollection<UserPotion> Users { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
}
