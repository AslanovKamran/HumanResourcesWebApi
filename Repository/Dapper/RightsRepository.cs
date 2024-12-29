using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.Rights;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class RightsRepository : IRightsRepository
{
    private readonly string _connectionString;
    public RightsRepository(string connectionString) => _connectionString = connectionString;

    #region Get
    public async Task<List<Right>> GetRightsListAsync()
    {
        var query = @"GetRightsList";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = await db.QueryAsync<Right>(query, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }


    public async Task<Right> GetRightByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"GetRightById";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryFirstOrDefaultAsync<Right>(query, parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }


    #endregion

    #region Add

    public async Task AddRightAsync(AddRightRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Key", request.Key, DbType.String, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"AddRight";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update

    public async Task UpdateRightByIdAsync(UpdateRightRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Key", request.Key, DbType.String, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"UpdateRight";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }


    #endregion
}
