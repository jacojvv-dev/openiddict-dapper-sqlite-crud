using CRUD.Api.Domain.Entities;

namespace CRUD.Api.Application.Interfaces;

public interface IShoppingListRepository
{
    Task<Guid> Add(Guid userId, string title);
    Task<int> Count(Guid userId);
    Task<IEnumerable<ShoppingListItem>> List(Guid userId, int page, int perPage);
    Task<ShoppingListItemDetail?> Find(Guid userId, Guid id);
    Task Update(Guid userId, Guid id, string title);
    Task Delete(Guid userId, Guid id);
}