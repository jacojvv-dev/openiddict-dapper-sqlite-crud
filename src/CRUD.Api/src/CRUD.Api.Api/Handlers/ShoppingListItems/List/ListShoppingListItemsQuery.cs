using CRUD.Api.Api.Responses;
using CRUD.Api.Api.Responses.ShoppingList;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.List;

public class ListShoppingListItemsQuery : IRequest<PaginatedResponse<ShoppingListItemResponse>>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
}