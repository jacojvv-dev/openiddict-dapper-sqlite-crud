using CRUD.Api.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IShoppingListService, ShoppingListService>();
        return services;
    }
}