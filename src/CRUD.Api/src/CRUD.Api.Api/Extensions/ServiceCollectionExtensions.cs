using System.Reflection;
using CRUD.Api.Api.Behaviours;
using CRUD.Api.Api.Mapping;
using CRUD.Api.Api.OperationFilters;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

namespace CRUD.Api.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection ConfigureFluentValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AutoMapperBase.AddMappings);
        return services;
    }

    public static IServiceCollection ConfigureOpenIddict(
        this IServiceCollection services,
        string issuer,
        string validIssuers)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        services.AddAuthorization();

        var issuers = validIssuers
            .Split(',')
            .Select(x => x.Trim());

        services
            .AddOpenIddict()
            .AddValidation(options =>
            {
                options.Configure(validationOptions =>
                {
                    validationOptions.TokenValidationParameters.ValidIssuers = issuers;
                });
                options.SetIssuer(issuer);
                options.UseSystemNetHttp();
                options.UseAspNetCore();
            });
        return services;
    }

    public static IServiceCollection ConfigureSwagger(
        this IServiceCollection services,
        string authorizationUrl,
        string tokenUrl)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(authorizationUrl),
                            TokenUrl = new Uri(tokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api", "API Scope" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

        return services;
    }
}