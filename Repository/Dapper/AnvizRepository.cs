using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class AnvizRepository : IAnvizRepository
{
    private readonly string _connectionString;

    public AnvizRepository(string coonectionString) => _connectionString = coonectionString;

    public async Task<List<Anviz>> GetCheckDateAsync(DateTime inTime, DateTime endTime)
    {
        var parameters = new DynamicParameters();
        parameters.Add("StartDate", inTime, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EndDate", endTime, DbType.DateTime, ParameterDirection.Input);

        var query = @"GetEmployeeCheckInOutGroupedByDateRange";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = await db.QueryAsync<Anviz>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
