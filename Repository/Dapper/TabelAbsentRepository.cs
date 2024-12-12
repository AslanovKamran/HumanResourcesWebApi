using Dapper;
using HumanResourcesWebApi.Models.Requests.TabelAbsent;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class TabelAbsentRepository : ITabelAbsentRepository
{
    private readonly string _connectionString;

    public TabelAbsentRepository(string connectionString) => _connectionString = connectionString;

    #region Get
    public async Task<List<TabelAbsent>> GetTabelAbsentsByIdAsync(int employeeId, int? year)
    {

        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Year", year, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectTabelAbsentByEmployeeAndYear";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<TabelAbsent>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Add

    public async Task AddTabelAbsentAsync(AddTabelAbsentRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Cause", request.Cause, DbType.String, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Bulletin", request.Bulletin, DbType.Int32, ParameterDirection.Input);
        parameters.Add("InsertedUser", request.InsertedUser, DbType.String, ParameterDirection.Input);

        var query = @"InsertTabelAbsent";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update



    public async Task UpdateTabelAbsentAsync(UpdateTabelAbsentRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Cause", request.Cause, DbType.String, ParameterDirection.Input);
        parameters.Add("Bulletin", request.Bulletin, DbType.Int32, ParameterDirection.Input);
        parameters.Add("UpdatedUser", request.UpdatedUser, DbType.String, ParameterDirection.Input);

        var query = @"UpdateTabelAbsent";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteTabelAbsentAsync(int id) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteTabelAbsentById";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    #endregion

}
