using System.Data;
using Dapper;

namespace CRUD.Api.Application.Interfaces;

public interface IDapperRepository
{
    Task ExecuteAsync(
        string query,
        DynamicParameters? parameters = null);

    Task ExecuteAsync(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null);

    Task<IEnumerable<T>> QueryAsync<T>(
        string query,
        DynamicParameters? parameters = null);

    Task<IEnumerable<T>> QueryAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null);

    Task<T> QuerySingleAsync<T>(
        string query,
        DynamicParameters? parameters = null);

    Task<T> QuerySingleAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null);

    Task<T?> QuerySingleOrDefaultAsync<T>(
        string query,
        DynamicParameters? parameters = null);

    Task<T?> QuerySingleOrDefaultAsync<T>(
        string query,
        CommandType commandType,
        DynamicParameters? parameters = null);
}