namespace PotionHouse.DataAccess.Entities;

#nullable disable

public partial class UserPotion
{
    public string ApplicationUserId { get; set; }
    public int PotionId { get; set; }

    public int Amount { get; set; }

    public ApplicationUser User { get; set; }
    public Potion Potion { get; set; }
}