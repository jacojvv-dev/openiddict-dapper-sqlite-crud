using System.Data;

namespace CRUD.Api.Application.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection GetConnection(string connectionStringName);
}