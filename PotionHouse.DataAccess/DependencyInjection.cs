using Microsoft.Extensions.DependencyInjection;
using PotionHouse.DataAccess.Repositories;
using PotionHouse.DataAccess.Repositories.Abstractions;

namespace PotionHouse.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IPotionsRepository, PotionsRepository>();
        services.AddScoped<IIngredientsRepository, IngredientsRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IRarityRepository, RarityRepository>();

        return services;
    }
}