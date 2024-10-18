using HumanResourcesWebApi.Models.Requests.PreviousNames;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class PreviousNamesRepository(string connectionString) : IPreviousNamesRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<PreviousName>> GetPreviousNamesAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "GetPreviousName";
        
        using (var db = new SqlConnection(_connectionString))
        {

            var result = await db.QueryAsync<PreviousName>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddPreviousNameAsync(AddPreviousNameRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);
        parameters.Add("ChangedAt", request.ChangedAt, DbType.Date, ParameterDirection.Input);

        var query = "AddPreviousName";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update
    public async Task UpdatePreviousNameAsync(UpdatePreviousNameRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);
        parameters.Add("ChangedAt", request.ChangedAt, DbType.Date, ParameterDirection.Input);

        var query = "UpdatePreviousName";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeletePreviousNameAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            
        var query = "DeletePreviousName";
        
        using (var db = new SqlConnection(_connectionString))
        {

            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
