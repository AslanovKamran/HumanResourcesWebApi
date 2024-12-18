using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests.TabelExtraWork;

namespace HumanResourcesWebApi.Repository.Dapper;
public class TabelExtraWorkRepository : ITabelExtraWorkRepository
{
    private readonly string _connectionString;
    public TabelExtraWorkRepository(string connectionString) => _connectionString = connectionString;

    #region Get

    public async Task<List<TabelExtraWorkDTO>> GetExtraWorkAsync(int employeeId, int year)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Year", year, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectTabelExtraWork";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<TabelExtraWorkDTO>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Add
    public async Task AddExtraWorkAsync(AddTabelExtraWorkRequest request)
    {
        var parameters = new DynamicParameters();
     
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ExtraWorkType", request.ExtraWorkType, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ExtraWorkHours", request.ExtraWorkHours, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ExtraWorkNightHours", request.ExtraWorkNightHours, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("InsertedUser", request.InsertedUser, DbType.String, ParameterDirection.Input);

        var query = @"InsertTabelExtraWork";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    #endregion

    #region Update

    public async Task UpdateExtraWorkAsync(UpdateTabelExtraWorkRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("ExtraWorkType", request.ExtraWorkType, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ExtraWorkHours", request.ExtraWorkHours, DbType.Int32, ParameterDirection.Input);
        parameters.Add("ExtraWorkNightHours", request.ExtraWorkNightHours, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("UpdatedUser", request.UpdatedUser, DbType.String, ParameterDirection.Input);

        var query = @"UpdateTabelExtraWork";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    
    public async Task DeleteExtraWorkByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteTabelExtraWork";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion
}
