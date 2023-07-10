using System.Data;
using CRUD.Api.Application.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CRUD.Api.Infrastructure.Factories;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection GetConnection(string connectionStringName)
        => new SqliteConnection(_configuration.GetConnectionString(connectionStringName));
}