using HumanResourcesWebApi.Models.Requests.Roles;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class RolesRepository : IRolesRepository
{
    private readonly string _connectionString;
    public RolesRepository(string connectionString) => _connectionString = connectionString;

    #region Add

    public async Task AddRoleAsync(AddRoleRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"AddRole";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Get

    public async Task<List<Role>> GetAllRolesAsync()
    {
        var query = @"GetRolesList";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<Role>(query, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Update

    public async Task UpdateRoleAsync(UpdateRoleRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"UpdateRole";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }


    #endregion
}
