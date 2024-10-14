using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.Awards;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class AwardsRepository : IAwardsRepository
{
    private readonly string _connectionString;

    public AwardsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Retrieve awards by EmployeeId
    public async Task<List<Award>> GetAwardsAsync(int employeeId)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

            var query = "GetAwards";
            var result = await db.QueryAsync<Award>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    // Add a new award
    public async Task AddAwardAsync(AddAwardRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
            parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);

            var query = "AddAward";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    // Update an existing award
    public async Task UpdateAwardAsync(UpdateAwardRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
            parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);

            var query = "UpdateAward";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    // Delete an award by Id
    public async Task DeleteAwardAsync(int id)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = "DeleteAward";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
