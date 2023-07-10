using CRUD.Identity.Web.Data;
using CRUD.Identity.Web.HostedServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Identity.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        => services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString);
                options.UseOpenIddict();
            })
            .AddDatabaseDeveloperPageExceptionFilter();

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();
        return services;
    }

    public static IServiceCollection ConfigureOpenIddict(this IServiceCollection services)
    {
        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddServer(options =>
            {
                options
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow()
                    .RequireProofKeyForCodeExchange();

                options
                    .SetAuthorizationEndpointUris("/connect/authorize")
                    .SetTokenEndpointUris("/connect/token");

                // if this was a production system I would not do the below
                options
                    .AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey()
                    .DisableAccessTokenEncryption();

                // similarly - if this was a production system I would not do the below
                // it is merely a matter of convenience
                options
                    .UseAspNetCore()
                    .DisableTransportSecurityRequirement();
                
                options.RegisterScopes("api");
                
                options
                    .UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough(); 
            });

        return services;
    }

    public static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<OpenIddictSeeder>();
        return services;
    }
}