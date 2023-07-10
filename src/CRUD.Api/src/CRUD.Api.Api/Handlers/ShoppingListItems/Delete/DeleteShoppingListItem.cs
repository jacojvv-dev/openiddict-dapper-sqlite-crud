using CRUD.Api.Api.Extensions;
using CRUD.Api.Application.Services;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Delete;

public class DeleteShoppingListItem
{
    public class Handler : IRequestHandler<DeleteShoppingListItemCommand, Unit>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingListService _shoppingListService;

        public Handler(IHttpContextAccessor accessor, IShoppingListService shoppingListService)
        {
            _accessor = accessor;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(
            DeleteShoppingListItemCommand command,
            CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext.GetUserId();
            await _shoppingListService.DeleteShoppingListItem(userId, command.Id);
            return Unit.Value;
        }
    }
}