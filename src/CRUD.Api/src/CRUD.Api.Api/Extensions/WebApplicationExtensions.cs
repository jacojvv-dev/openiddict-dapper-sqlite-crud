using CRUD.Api.Api.Handlers.ShoppingListItems.Create;
using CRUD.Api.Api.Handlers.ShoppingListItems.Delete;
using CRUD.Api.Api.Handlers.ShoppingListItems.Find;
using CRUD.Api.Api.Handlers.ShoppingListItems.List;
using CRUD.Api.Api.Handlers.ShoppingListItems.Update;
using CRUD.Api.Api.Requests.ShoppingList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Api.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication MapEndpoints(this WebApplication application)
    {
        application
            .MapGroup("api")
            .MapApi();
        return application;
    }

    private static void MapApi(this RouteGroupBuilder group)
    {
        group
            .MapGroup("list")
            .MapListApi()
            .RequireAuthorization()
            .WithOpenApi();
    }

    private static RouteGroupBuilder MapListApi(this RouteGroupBuilder route)
    {
        route.MapGet("/", [Authorize] async (
            [FromQuery] int? page,
            [FromQuery] int? perPage,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new ListShoppingListItemsQuery()
            {
                Page = page,
                PerPage = perPage
            };
            var result = await mediator.Send(query, cancellationToken);
            return Results.Ok(result);
        });

        route.MapGet("/{id}", [Authorize] async (
                [FromRoute] Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new FindShoppingListItemQuery()
                {
                    Id = id
                }, cancellationToken);
                return result == null ? Results.NotFound() : Results.Ok(result);
            })
            .WithName("GetListItemById");

        route.MapPut("/{id}", [Authorize] async (
            [FromRoute] Guid id,
            [FromBody] UpdateShoppingListItemRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateShoppingListItemCommand()
            {
                Id = id,
                Title = request.Title
            }, cancellationToken);
            return Results.Accepted();
        });

        route.MapDelete("/{id}", [Authorize] async (
            [FromRoute] Guid id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeleteShoppingListItemCommand()
            {
                Id = id,
            }, cancellationToken);
            return Results.Ok();
        });

        route.MapPost("/", [Authorize] async (
            [FromBody] CreateShoppingListItemCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return Results.CreatedAtRoute("GetListItemById", new { id = result });
        });

        return route;
    }
}