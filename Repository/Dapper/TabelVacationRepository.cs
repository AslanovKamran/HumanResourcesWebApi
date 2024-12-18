using HumanResourcesWebApi.Models.Requests.TabelVacation;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Azure.Core;

namespace HumanResourcesWebApi.Repository.Dapper;

public class TabelVacationRepository : ITabelVacationRepository
{
    private readonly string _connectionString;
    public TabelVacationRepository(string connectionString) => _connectionString = connectionString;

    #region Get

    public async Task<List<TabelVacationDTO>> GetTabelVacationsAsync(int employeeId, int? beginYear, int? endYear)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BeginYear", beginYear, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EndYear", endYear, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectTabelVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<TabelVacationDTO>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Add

    public async Task AddTabelVacationAsync(AddTabelVacationRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("BeginDate", request.BeginDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EndDate", request.EndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("MainDay", request.MainDay, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("RecalDate", request.RecalDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("InsertedUser", request.InsertedUser, DbType.String, ParameterDirection.Input);

        var query = @"InsertTabelVacation";

        using (var db = new SqlConnection(_connectionString)) 
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update

    public async Task UpdateTabelVacationAsync(UpdateTabelVacationRequest request) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BeginDate", request.BeginDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EndDate", request.EndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("MainDay", request.MainDay, DbType.Int32, ParameterDirection.Input);
        parameters.Add("RecalDate", request.RecalDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("OrderDate", request.OrderDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("UpdatedUser", request.UpdatedUser, DbType.String, ParameterDirection.Input);

        var query = @"UpdateTabelVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete

    public async Task DeleteTabelVacationAsync(int id) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteTabelVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }


    #endregion
}
