using PotionHouse.DataAccess.Entities.Common;

namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class Rarity : BaseEntity
{
    public string Title { get; set; }
    public string BgColorHex { get; set; }
    public string TextColorHex { get; set; }
}
// leg#ffc107 myth #6610f2