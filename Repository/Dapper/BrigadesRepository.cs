using System.Data;
using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Brigades;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Repository.Dapper;

public class BrigadesRepository : IBrigadesRepository
{
    private readonly string _connectionString;
    public BrigadesRepository(string connectionString) => _connectionString = connectionString;



    public async Task<List<Brigade>> GetBrigadesAsync()
    {
        var query = @"GetBrigades";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<Brigade>(query, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    public async Task<Brigade> GetBrigadeByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"GetBrigadeById";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QuerySingleOrDefaultAsync<Brigade>(query, parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }

    public async Task AddBrigadeAsync(AddBrigadeRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("FirstDate", request.FirstDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("SecondDate", request.SecondDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("WorkingHours", request.WorkingHours, DbType.Int32, ParameterDirection.Input);

        var query = @"CreateBrigade";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UpdateBrigadeAsync(UpdateBrigadeRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("FirstDate", request.FirstDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("SecondDate", request.SecondDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("WorkingHours", request.WorkingHours, DbType.Int32, ParameterDirection.Input);

        var query = @"UpdateBrigade";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteBrigadeAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteBrigade";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    public async Task AssignNewBrigadeToEmployeeAsync(int employeeId, int brigadeId)
    {
        var parameters = new DynamicParameters();

        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BrigadeId", brigadeId, DbType.Int32, ParameterDirection.Input);

        var query = "AssignNewBrigadeToEmployee";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UnassignFromAllBrigades(int employeeId)
    {
        var parameters = new DynamicParameters();

        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteBrigadeFromEmployee";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<List<BrigadeDto>> GetEmployeesByBrigadeId(int id)
    {
        var parameters = new DynamicParameters();

        parameters.Add("BrigadeId", id, DbType.Int32, ParameterDirection.Input);

        var query = @"GetEmployeesByBrigadeId";
        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<BrigadeDto>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

    }
}
