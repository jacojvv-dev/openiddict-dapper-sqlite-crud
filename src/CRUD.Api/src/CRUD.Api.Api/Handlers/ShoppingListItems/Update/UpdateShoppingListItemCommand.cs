using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Update;

public class UpdateShoppingListItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}