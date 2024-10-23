using HumanResourcesWebApi.Models.Requests.Awards;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

namespace HumanResourcesWebApi.Repository.Dapper;

public class AwardsRepository(string connectionString) : IAwardsRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<EmloyeeAward>> GetAwardsAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "GetAwards";
        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<EmloyeeAward>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddAwardAsync(AddAwardRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);
        using (var db = new SqlConnection(_connectionString))
        {

            var query = "AddAward";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateAwardAsync(UpdateAwardRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);

        var query = "UpdateAward";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }


    #endregion

    #region Delete
    public async Task DeleteAwardAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteAward";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
