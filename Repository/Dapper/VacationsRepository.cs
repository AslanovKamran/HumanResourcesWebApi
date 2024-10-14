using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Vacations;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class VacationsRepository : IVacationsRepository
{
    private readonly string _connectionString;

    public VacationsRepository(string connectionString) => _connectionString = connectionString;

    public async Task<List<EmployeeVacation>> GetVacationsAsync(int employeeId, int? yearStarted = null, int? yearEnded = null)
    {
        var parameters = new DynamicParameters();

        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearStarted", yearStarted, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearEnded", yearEnded, DbType.Int32, ParameterDirection.Input);

        var query = @"GetVacations";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = (await db.QueryAsync<EmployeeVacation>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
            return result;
        }
    }


    public async Task AddVacationAsync(AddVacationRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DaysWorking", request.DaysWorking, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DaysTotal", request.DaysTotal, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearStarted", request.YearStarted, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearEnded", request.YearEnded, DbType.Int32, ParameterDirection.Input);
        parameters.Add("VacationTypeId", request.VacationTypeId, DbType.Int32, ParameterDirection.Input);

        var query = "AddVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UpdateVacationAsync(UpdateVacationRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DaysWorking", request.DaysWorking, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DaysTotal", request.DaysTotal, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearStarted", request.YearStarted, DbType.Int32, ParameterDirection.Input);
        parameters.Add("YearEnded", request.YearEnded, DbType.Int32, ParameterDirection.Input);
        parameters.Add("VacationTypeId", request.VacationTypeId, DbType.Int32, ParameterDirection.Input);

        var query = "UpdateVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteVacationAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
