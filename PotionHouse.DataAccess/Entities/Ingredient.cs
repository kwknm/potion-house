using PotionHouse.DataAccess.Entities.Common;

namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class Ingredient : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public ICollection<Potion> Potions { get; set; }
}
