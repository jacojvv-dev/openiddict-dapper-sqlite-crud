using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Delete;

public class DeleteShoppingListItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}