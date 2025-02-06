using HumanResourcesWebApi.Models.Requests.BrigadeReplacements;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Azure.Core;

namespace HumanResourcesWebApi.Repository.Dapper;

public class BrigadeReplacementsRepository : IBrigadeReplacementsRepository
{
    private readonly string _connectionString;

    public BrigadeReplacementsRepository(string connectionString) => _connectionString = connectionString;
    public async Task<List<BrigadeReplacement>> GetBrigadeReplacementsAsync()
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var query = "GetBrigadeReplacements";

            var result = await db.QueryAsync<BrigadeReplacement>(query, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    public async Task AddBrigadeReplacementAsync(AddBrigadeReplacementRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("FirstBrigadeId", request.FirstBrigadeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("SecondBrigadeId", request.SecondBrigadeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("StartDate", request.StartDate, DbType.DateTime2, ParameterDirection.Input);
        parameters.Add("EndDate", request.EndDate, DbType.DateTime2, ParameterDirection.Input);

        var query = "AddBrigadeReplacement";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteBrigadeReplacementAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteBrigadeReplacement";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
