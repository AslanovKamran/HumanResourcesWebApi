using HumanResourcesWebApi.Models.Requests.Medals;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class MedalsRepository : IMedalsRepository
{
    private readonly string _connectionString;
    public MedalsRepository(string connectionString) => _connectionString = connectionString;

    public async Task AddMedalAsync(AddMedalRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderDate", request.OrderDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
            parameters.Add("MedalTypeId", request.MedalTypeId, DbType.Int32, ParameterDirection.Input);

            var query = "AddMedal";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<List<EmployeeMedal>> GetMedalsAsync(int employeeId)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

            var query = "GetMedals";
            var result = await db.QueryAsync<EmployeeMedal>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    public async Task UpdateMedalAsync(UpdateMedalRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderDate", request.OrderDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("MedalTypeId", request.MedalTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);

            var query = "UpdateMedal";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteMedalAsync(int id)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = "DeleteMedal";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
