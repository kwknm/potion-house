using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Dtos;

public record IngredientWithAmount(
    Ingredient Details,
    int Amount);