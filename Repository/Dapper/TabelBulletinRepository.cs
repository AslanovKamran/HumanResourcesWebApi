using Azure.Core;
using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.TabelBulletin;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class TabelBulletinRepository : ITabelBulletinRepository
{
    private readonly string _connectionString;

    public TabelBulletinRepository(string connectionString) => _connectionString = connectionString;


    #region Get

    public async Task<List<TabelBulletinDTO>> GetTabelBulletinsAsync(int employeeId, int? beginYear, int? endYear)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BeginYear", beginYear, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EndYear", endYear, DbType.Int32, ParameterDirection.Input);

        var query = @"SelectTabelBulletin";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<TabelBulletinDTO>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

    }

    #endregion

    #region Add

    public async Task AddTabelBulletinAsync(AddTabelBulletinRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Serial", request.Serial, DbType.String, ParameterDirection.Input);
        parameters.Add("Number", request.Number, DbType.String, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("InvalidityBeginDate", request.InvalidityBeginDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("InvalidityEndDate", request.InvalidityEndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("InvalidityContinues", request.InvalidityContinues, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@InsertedUser", request.InsertedUser, DbType.String, ParameterDirection.Input);

        var query = @"AddTabelBulletin";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update

    public async Task UpdateTabelBulletinAsync(UpdateTabelBulletinRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Serial", request.Serial, DbType.String, ParameterDirection.Input);
        parameters.Add("Number", request.Number, DbType.String, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("InvalidityBeginDate", request.InvalidityBeginDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("InvalidityEndDate", request.InvalidityEndDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("InvalidityContinues", request.InvalidityContinues, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@UpdatedUser", request.UpdatedUser, DbType.String, ParameterDirection.Input);

        var query = @"UpdateTabelBulletin";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    public async Task DeleteTabelBulletinByIdAsync(int  id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteTabelBulletin";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
