using AutoMapper;
using CRUD.Api.Api.Extensions;
using CRUD.Api.Api.Responses.ShoppingList;
using CRUD.Api.Application.Services;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.Find;

public class FindShoppingListItem
{
    public class Handler : IRequestHandler<FindShoppingListItemQuery, ShoppingListItemDetailResponse?>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingListService _shoppingListService;

        public Handler(IMapper mapper, IHttpContextAccessor accessor, IShoppingListService shoppingListService)
        {
            _mapper = mapper;
            _accessor = accessor;
            _shoppingListService = shoppingListService;
        }

        public async Task<ShoppingListItemDetailResponse?> Handle(
            FindShoppingListItemQuery query,
            CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext.GetUserId();
            var item = await _shoppingListService.FindShoppingListItem(userId, query.Id);
            return item == default ? null : _mapper.Map<ShoppingListItemDetailResponse>(item);
        }
    }
}