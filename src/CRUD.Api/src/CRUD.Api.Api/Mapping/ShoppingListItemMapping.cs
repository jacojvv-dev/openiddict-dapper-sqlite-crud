using AutoMapper;
using CRUD.Api.Api.Responses.ShoppingList;
using CRUD.Api.Domain.Entities;

namespace CRUD.Api.Api.Mapping;

public class ShoppingListItemMapping : Profile
{
    public ShoppingListItemMapping()
    {
        CreateMap<ShoppingListItem, ShoppingListItemResponse>();
        CreateMap<ShoppingListItemDetail, ShoppingListItemDetailResponse>();
    }
}