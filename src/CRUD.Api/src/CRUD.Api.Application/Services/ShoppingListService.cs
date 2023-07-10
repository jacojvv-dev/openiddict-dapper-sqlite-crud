using CRUD.Api.Application.Interfaces;
using CRUD.Api.Domain.Entities;

namespace CRUD.Api.Application.Services;

public interface IShoppingListService
{
    Task<Guid> AddShoppingListItem(Guid userId, string title);
    Task<int> CountShoppingListItems(Guid userId);
    Task<IEnumerable<ShoppingListItem>> GetShoppingListItems(Guid userId, int page, int perPage);
    Task<ShoppingListItemDetail?> FindShoppingListItem(Guid userId, Guid id);
    Task UpdateShoppingListItem(Guid userId, Guid id, string title);
    Task DeleteShoppingListItem(Guid userId, Guid id);
}

public class ShoppingListService : IShoppingListService
{
    private readonly IShoppingListRepository _repository;

    public ShoppingListService(IShoppingListRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> AddShoppingListItem(Guid userId, string title)
        => await _repository.Add(userId, title);

    public async Task<int> CountShoppingListItems(Guid userId)
        => await _repository.Count(userId);

    public async Task<IEnumerable<ShoppingListItem>> GetShoppingListItems(Guid userId, int page, int perPage)
        => await _repository.List(userId, page, perPage);

    public async Task<ShoppingListItemDetail?> FindShoppingListItem(Guid userId, Guid id)
        => await _repository.Find(userId, id);

    public async Task UpdateShoppingListItem(Guid userId, Guid id, string title)
        => await _repository.Update(userId, id, title);

    public async Task DeleteShoppingListItem(Guid userId, Guid id)
        => await _repository.Delete(userId, id);
}