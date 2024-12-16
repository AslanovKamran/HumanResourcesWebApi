using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class AnvizEmployeesRepository : IAnvizEmployeesRepository
{
    private readonly string _connectionString;
    public AnvizEmployeesRepository(string connectionString) => _connectionString = connectionString;

    public  async Task<List<AnvizEmployee>> GetAnvizEmployeesAsync(int? organizationStructureId)
    {

        var parameters = new DynamicParameters();
        parameters.Add("OrganizationStructureId", organizationStructureId, DbType.Int32, ParameterDirection.Input);

        var query = "GetAnvizEmployeeData";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = await db.QueryAsync<AnvizEmployee>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


    }
}
