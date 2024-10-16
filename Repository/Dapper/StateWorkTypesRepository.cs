using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class StateWorkTypesRepository : IStateWorkTypesRepository
{
    private readonly string _connectionString;

    public StateWorkTypesRepository(string connectionString) => _connectionString = connectionString;

    public async Task<StateWorkType> GetStateWorkTypeByIdAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            string query = @"SELECT * FROM StateWorkTypes WHERE Id = @Id";
            var workType = await db.QueryFirstOrDefaultAsync<StateWorkType>(query, parameters);
            return workType!;
        }
    }

    public async Task<List<StateWorkType>> GetStateWorkTypesAsync()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var query = @"SELECT * FROM StateWorkTypes ORDER BY Id";
            return (await db.QueryAsync<StateWorkType>(query)).ToList();
        }
    }
}
