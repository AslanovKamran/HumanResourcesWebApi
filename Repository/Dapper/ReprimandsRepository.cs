using HumanResourcesWebApi.Models.Requests.Reprimands;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class ReprimandsRepository(string connectionString) : IReprimandsRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<EmployeeReprimand>> GetReprimandsAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "GetReprimands";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = (await db.QueryAsync<EmployeeReprimand>(query, parameters, commandType: CommandType.StoredProcedure));
            return result.AsList();
        }
    }

    #endregion

    #region Add

    public async Task AddReprimandAsync(AddReprimandRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("IssuedAt", request.IssuedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("TakenAt", request.TakenAt ?? null, DbType.Date, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);

        var query = "AddReprimand";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateReprimandAsync(UpdateReprimandRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("IssuedAt", request.IssuedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);
        parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);
        parameters.Add("TakenAt", request.TakenAt ?? null, DbType.Date, ParameterDirection.Input);

        var query = "UpdateReprimand";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteReprimandAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteReprimand";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
