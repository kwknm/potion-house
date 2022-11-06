using PotionHouse.DataAccess.Entities;

namespace PotionHouse.DataAccess.Dtos;

public record PotionWithAmount(
    Potion Details,
    int Amount);