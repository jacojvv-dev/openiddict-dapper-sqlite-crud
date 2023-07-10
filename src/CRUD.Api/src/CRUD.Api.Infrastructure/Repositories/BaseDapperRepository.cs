using System.Data;
using CRUD.Api.Application.Interfaces;
using Dapper;

namespace CRUD.Api.Infrastructure.Repositories;

public abstract class BaseDapperRepository : IDapperRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    protected abstract string ConnectionStringName { get; }

    protected BaseDapperRepository(ISqlConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<T>>
        QueryAsync<T>(string query, DynamicParameters? parameters = null)
        => await QueryAsync<T>(query, CommandType.StoredProcedure, parameters);
    
    public async Task<IEnumerable<T>> QueryAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null)
    {
        using var db = _connectionFactory.GetConnection(ConnectionStringName);
        return await db.QueryAsync<T>(query, param: parameters, commandType: commandType);
    }

    public async Task<T> QuerySingleAsync<T>(string query, DynamicParameters? parameters = null)
        => await QuerySingleAsync<T>(query, CommandType.StoredProcedure, parameters);

    public async Task<T> QuerySingleAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null)
    {
        using var db = _connectionFactory.GetConnection(ConnectionStringName);
        return await db.QuerySingleAsync<T>(query, param: parameters, commandType: commandType);
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string query, DynamicParameters? parameters = null)
        => await QuerySingleOrDefaultAsync<T>(query, CommandType.StoredProcedure, parameters);

    public async Task<T?> QuerySingleOrDefaultAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null)
    {
        using var db = _connectionFactory.GetConnection(ConnectionStringName);
        return await db.QuerySingleOrDefaultAsync<T>(query, param: parameters, commandType: commandType);
    }

    public async Task ExecuteAsync(string query, DynamicParameters? parameters = null)
        => await ExecuteAsync(query, CommandType.StoredProcedure, parameters);

    public async Task ExecuteAsync(string query, CommandType commandType, DynamicParameters? parameters = null)
    {
        using var db = _connectionFactory.GetConnection(ConnectionStringName);
        await db.ExecuteAsync(query, param: parameters, commandType: commandType);
    }
}