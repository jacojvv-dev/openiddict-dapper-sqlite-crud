using CRUD.Api.Api.Extensions;
using CRUD.Api.Application.Services;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Create;

public class CreateShoppingListItem
{
    public class Handler : IRequestHandler<CreateShoppingListItemCommand, Guid>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingListService _shoppingListService;

        public Handler(IHttpContextAccessor accessor, IShoppingListService shoppingListService)
        {
            _accessor = accessor;
            _shoppingListService = shoppingListService;
        }

        public async Task<Guid> Handle(
            CreateShoppingListItemCommand command,
            CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext.GetUserId();
            return await _shoppingListService.AddShoppingListItem(userId, command.Title);
        }
    }
}