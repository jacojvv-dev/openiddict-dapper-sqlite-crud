using AutoMapper;
using CRUD.Api.Api.Extensions;
using CRUD.Api.Api.Responses;
using CRUD.Api.Api.Responses.ShoppingList;
using CRUD.Api.Application.Services;
using MediatR;

namespace CRUD.Api.Api.Handlers.ShoppingListItems.List;

public class ListShoppingListItems
{
    public class Handler : IRequestHandler<ListShoppingListItemsQuery, PaginatedResponse<ShoppingListItemResponse>>
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

        public async Task<PaginatedResponse<ShoppingListItemResponse>> Handle(
            ListShoppingListItemsQuery query,
            CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext.GetUserId();

            var page = Math.Clamp(query.Page ?? 1, 1, int.MaxValue);
            var perPage = Math.Clamp(query.PerPage ?? 25, 1, 50);

            var total = await _shoppingListService.CountShoppingListItems(userId);
            var items = await _shoppingListService.GetShoppingListItems(userId, page, perPage);

            return new PaginatedResponse<ShoppingListItemResponse>(
                _mapper.Map<List<ShoppingListItemResponse>>(items),
                page,
                perPage,
                total
            );
        }
    }
}