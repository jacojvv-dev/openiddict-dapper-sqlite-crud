using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Create;

public class CreateShoppingListItemCommand : IRequest<Guid>
{
    public string Title { get; set; }
}