using CRUD.Api.Api.Extensions;
using CRUD.Api.Application.Services;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Update;

public class UpdateShoppingListItem
{
    public class Handler : IRequestHandler<UpdateShoppingListItemCommand, Unit>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingListService _shoppingListService;

        public Handler(IHttpContextAccessor accessor, IShoppingListService shoppingListService)
        {
            _accessor = accessor;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(
            UpdateShoppingListItemCommand command,
            CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext.GetUserId();
            await _shoppingListService.UpdateShoppingListItem(userId, command.Id, command.Title);
            return Unit.Value;
        }
    }
}