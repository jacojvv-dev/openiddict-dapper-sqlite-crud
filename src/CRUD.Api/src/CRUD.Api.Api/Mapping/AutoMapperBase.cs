using AutoMapper;

namespace CRUD.Api.Api.Mapping;

internal class AutoMapperBase : Profile
{
    public AutoMapperBase()
    {
    }

    public static void AddMappings(IMapperConfigurationExpression configuration)
    {
        configuration.AddProfile<AutoMapperBase>();
        configuration.AddProfile<ShoppingListItemMapping>();
    }
}