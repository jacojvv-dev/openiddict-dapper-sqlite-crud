using System.Data;
using CRUD.Api.Application.Interfaces;
using CRUD.Api.Domain.Entities;
using Dapper;

namespace CRUD.Api.Infrastructure.Repositories;

public class ShoppingListRepository : BaseDapperRepository, IShoppingListRepository
{
    protected override string ConnectionStringName => "DefaultConnection";

    public ShoppingListRepository(ISqlConnectionFactory connectionFactory) : base(connectionFactory)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<Guid> Add(Guid userId, string title)
    {
        var id = Guid.NewGuid();

        const string query = @"INSERT INTO shopping_list_items (id, title, user_id, created_at, updated_at)
                                VALUES (@id, @title, @user_id, datetime('now'), datetime('now'))";

        var @params = new DynamicParameters();
        @params.Add("id", id);
        @params.Add("user_id", userId);
        @params.Add("title", title);

        await ExecuteAsync(query, CommandType.Text, @params);

        return id;
    }

    public async Task<int> Count(Guid userId)
    {
        const string query = @"SELECT COUNT(id)
                      FROM shopping_list_items
                      WHERE user_id = @user_id";

        var @params = new DynamicParameters();
        @params.Add("user_id", userId);

        return await QuerySingleOrDefaultAsync<int>(query, CommandType.Text, @params);
    }

    public async Task<IEnumerable<ShoppingListItem>> List(Guid userId, int page, int perPage)
    {
        const string query = @"SELECT id, title 
                      FROM shopping_list_items
                      WHERE user_id = @user_id
                      ORDER BY created_at ASC
                      LIMIT @per_page OFFSET @per_page * (@page - 1)";

        var @params = new DynamicParameters();
        @params.Add("user_id", userId);
        @params.Add("page", page);
        @params.Add("per_page", perPage);

        return await QueryAsync<ShoppingListItem>(query, CommandType.Text, @params);
    }

    public async Task<ShoppingListItemDetail?> Find(Guid userId, Guid id)
    {
        const string query = @"SELECT id, title, updated_at, created_at
                      FROM shopping_list_items
                      WHERE id = @id AND user_id = @user_id";

        var @params = new DynamicParameters();
        @params.Add("id", id);
        @params.Add("user_id", userId);

        return await QuerySingleOrDefaultAsync<ShoppingListItemDetail>(query, CommandType.Text, @params);
    }

    public async Task Update(Guid userId, Guid id, string title)
    {
        const string query = @"UPDATE shopping_list_items 
                      SET title = @title, updated_at = datetime('now')
                      WHERE id = @id AND user_id = @user_id";

        var @params = new DynamicParameters();
        @params.Add("id", id);
        @params.Add("title", title);
        @params.Add("user_id", userId);

        await ExecuteAsync(query, CommandType.Text, @params);
    }

    public async Task Delete(Guid userId, Guid id)
    {
        const string query = @"DELETE FROM shopping_list_items
                      WHERE id = @id AND user_id = @user_id";

        var @params = new DynamicParameters();
        @params.Add("id", id);
        @params.Add("user_id", userId);

        await ExecuteAsync(query, CommandType.Text, @params);
    }
}