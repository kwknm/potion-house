namespace PotionHouse.DataAccess.Entities;

#nullable disable

public class UserIngredients
{
    public string ApplicationUserId { get; set; }
    public int IngredientId { get; set; }

    public int Amount { get; set; }

    public ApplicationUser User { get; set; }
    public Ingredient Ingredient { get; set; }
}