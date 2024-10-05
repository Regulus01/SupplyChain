using System.Data;
using Dapper;
using SupplyChain.Domain.Interface.Repository;

namespace SupplyChain.Infra.Data.Repository;

public class DatabaseService : IDatabaseService
{
    private readonly IDbConnection _dbConnection;

    public DatabaseService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<T> Query<T>(string query) where T : class
    {
        var items = _dbConnection.Query<T>(query);
        
        _dbConnection.Close();

        return items;
    }
}