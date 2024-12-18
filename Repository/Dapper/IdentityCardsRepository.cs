using HumanResourcesWebApi.Models.Requests.IdentityCards;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class IdentityCardsRepository(string connectionString) : IIdentityCardsRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<IdentityCard> GetIdentityCardAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "GetIdentityCard";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QuerySingleOrDefaultAsync<IdentityCard>(query, parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }

    #endregion

    #region Add
    public async Task AddIdentityCardAsync(AddIdentityCardRequest request)
    {
        var parameters = new DynamicParameters();
       
        parameters.Add("Series", request.Series, DbType.String, ParameterDirection.Input);
        parameters.Add("CardNumber", request.CardNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Organization", request.Organization, DbType.String, ParameterDirection.Input);
        parameters.Add("GivenAt", request.GivenAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("ValidUntil", request.ValidUntil, DbType.Date, ParameterDirection.Input);
        parameters.Add("FinCode", request.FinCode, DbType.String, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("PhotoFront", request.PhotoFront, DbType.String, ParameterDirection.Input);
        parameters.Add("PhotoBack", request.PhotoBack, DbType.String, ParameterDirection.Input);

        var query = "AddIdentityCard";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateIdentityCardAsync(UpdateIdentityCardRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Series", request.Series, DbType.String, ParameterDirection.Input);
        parameters.Add("CardNumber", request.CardNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("Organization", request.Organization, DbType.String, ParameterDirection.Input);
        parameters.Add("GivenAt", request.GivenAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("ValidUntil", request.ValidUntil, DbType.Date, ParameterDirection.Input);
        parameters.Add("FinCode", request.FinCode, DbType.String, ParameterDirection.Input);
        parameters.Add("PhotoFront", request.PhotoFront, DbType.String, ParameterDirection.Input);
        parameters.Add("PhotoBack", request.PhotoBack, DbType.String, ParameterDirection.Input);

        var query = "UpdateIdentityCard";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
