using CRUD.Api.Api.Extensions;
using CRUD.Api.Application.Extensions;
using CRUD.Api.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder
    .Services
    .ConfigureSwagger(
        configuration["SwaggerConfiguration:OAuthAuthorizationEndpoint"],
        configuration["SwaggerConfiguration:OAuthTokenEndpoint"]
    );

builder
    .Services
    .AddHttpContextAccessor()
    .ConfigureMediatR()
    .ConfigureFluentValidations()
    .ConfigureAutoMapper();

builder
    .Services
    .ConfigureServices()
    .ConfigureRepositories();

builder.Services
    .ConfigureOpenIddict(
        configuration["IdentityConfiguration:Issuer"],
        configuration["IdentityConfiguration:ValidIssuers"]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId(configuration["SwaggerConfiguration:OAuthClientId"]);
    c.OAuthClientSecret(configuration["SwaggerConfiguration:OAuthClientSecret"]);
    c.OAuthUsePkce();
});

app.UseAuthorization();
app.MapEndpoints();

app.Run();