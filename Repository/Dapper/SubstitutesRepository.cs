using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Substitutes;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class SubstitutesRepository : ISubstitutesRepository
{
    private readonly string _connectionString;
    public SubstitutesRepository(string connectionString) => _connectionString = connectionString;

    #region Get

    public async Task<List<SubstituteByVacationDTO>> GetSubstituteByVacationsAsync(int whomId, int vacationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("WhomId", whomId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("TabelVacationId", vacationId, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectSubstitutesByVacation";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<SubstituteByVacationDTO>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    public async Task<List<SubstituteByBulletinDTO>> GetSubstituteByBulletinAsync(int whomId, int bulletinId) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("WhomId", whomId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BulletinId", bulletinId, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectSubstitutesByBulletin";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<SubstituteByBulletinDTO>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Add
    public async Task AddSubstituteAsync(AddSubstituteRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WhoId", request.WhoId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WhomId", request.WhomId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("TabelVacationId", request.TabelVacationId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BulletinId", request.BulletinId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("SubStartDate", request.SubStartDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("SubEndDate", request.SubEndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("InsertedUser", request.InsertedUser, DbType.String, ParameterDirection.Input);

        var query = "InsertSubstitute";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update
    public async Task UpdateSubstituteAsync(UpdateSubstituteRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WhoId", request.WhoId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("SubStartDate", request.SubStartDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("SubEndDate", request.SubEndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("UpdatedUser", request.UpdatedUser, DbType.String, ParameterDirection.Input);

        var query = @"UpdateSubstitute";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete

    public async Task DeleteSubstituteByIdAsync(int id) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteSubstitute";

        using (var db = new SqlConnection(_connectionString)) 
        {
            await db.ExecuteAsync(query, parameters,commandType: CommandType.StoredProcedure);
        }
    }

    #endregion


}
