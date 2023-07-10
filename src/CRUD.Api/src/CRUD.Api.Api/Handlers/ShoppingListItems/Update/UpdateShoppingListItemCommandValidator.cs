using FluentValidation;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Update;

public class UpdateShoppingListItemCommandValidator : AbstractValidator<UpdateShoppingListItemCommand>
{
    public UpdateShoppingListItemCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}