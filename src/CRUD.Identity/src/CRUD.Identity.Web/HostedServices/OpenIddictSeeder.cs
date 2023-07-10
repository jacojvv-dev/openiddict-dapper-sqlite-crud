using CRUD.Identity.Web.Data;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace CRUD.Identity.Web.HostedServices;

public class OpenIddictSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public OpenIddictSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("swagger", cancellationToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "swagger",
                ClientSecret = "",
                DisplayName = "swagger",
                RedirectUris = { new Uri("http://localhost:5260/swagger/oauth2-redirect.html"), new Uri("http://localhost:5001/swagger/oauth2-redirect.html") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",

                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}