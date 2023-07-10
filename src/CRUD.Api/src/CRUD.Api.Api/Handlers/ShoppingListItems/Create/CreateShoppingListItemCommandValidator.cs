using FluentValidation;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Create;

public class CreateShoppingListItemCommandValidator : AbstractValidator<CreateShoppingListItemCommand>
{
    public CreateShoppingListItemCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}