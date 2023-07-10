using CRUD.Api.Api.Responses.ShoppingList;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Find;

public class FindShoppingListItemQuery : IRequest<ShoppingListItemDetailResponse?>
{
    public Guid Id { get; set; }
}