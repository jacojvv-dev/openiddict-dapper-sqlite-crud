using CRUD.Api.Application.Interfaces;
using CRUD.Api.Infrastructure.Factories;
using CRUD.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddSingleton<IShoppingListRepository, ShoppingListRepository>();
        return services;
    }
}