using HumanResourcesWebApi.Models.Requests.WorkActivities;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class WorkActivitiesRepository : IWorkActivitiesRepository
{
    private readonly string _connectionString;

    public WorkActivitiesRepository(string connectionString) => _connectionString = connectionString;
    public async Task<List<EmployeeWorkActivity>> GetEmployeeWorkActivityAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = @"GetWorkActivities";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = (await db.QueryAsync<EmployeeWorkActivity>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
            return result;
        }
    }
    public async Task UpdateWorkActivityAsync(UpdateWorkActivityRequest request)
    {
        var query = "UpdateWorkActivity"; // Stored procedure name

        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkActivityTypeId", request.WorkActivityTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkActivityDate", request.WorkActivityDate, DbType.Date, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("WorkActivityReason", request.WorkActivityReason, DbType.String, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("NewStateTableId", request.NewStateTableId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkShiftTypeId", request.WorkShiftTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkDayOffId", request.WorkDayOffId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkShiftStartedAt", request.WorkShiftStartedAt, DbType.Date, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task DeleteWorkActivityAsync(int id)
    {
        var query = "DeleteWorkActivity"; // Stored procedure name

        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task AddWorkActivityAsync(AddWorkActivityRequest request)
    {
        var query = "AddWorkActivity"; // Stored procedure name

        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkActivityTypeId", request.WorkActivityTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkActivityDate", request.WorkActivityDate, DbType.Date, ParameterDirection.Input);
        parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("WorkActivityReason", request.WorkActivityReason, DbType.String, ParameterDirection.Input);
        parameters.Add("NewStateTableId", request.NewStateTableId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkShiftTypeId", request.WorkShiftTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkDayOffId", request.WorkDayOffId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkShiftStartedAt", request.WorkShiftStartedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
