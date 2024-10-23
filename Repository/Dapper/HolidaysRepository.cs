using HumanResourcesWebApi.Models.Requests.Holidays;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class HolidaysRepository(string connectionString) : IHolidaysRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<HolidayDTO>> GetHolidaysAsync(int? year)
    {
        year ??= DateTime.Now.Year;

        var parameters = new DynamicParameters();
        parameters.Add("Date", year, DbType.Int32, ParameterDirection.Input);

        var query = @"GetHolidays";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = await db.QueryAsync<HolidayDTO>(query, parameters, commandType:CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddHolidayAsync(AddHolidayRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Date",request.Date, DbType.Date, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input); 
        parameters.Add("HolidayTypeId",request.HolidayTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("HolidayForShiftId", request.HolidayForShiftId, DbType.Int32, ParameterDirection.Input);

        var query = @"AddHoliday";

        using (var db = new SqlConnection(_connectionString)) 
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateHolidayAsync(UpdateHolidayRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Date", request.Date, DbType.Date, ParameterDirection.Input);
        parameters.Add("Note", request.Note, DbType.String, ParameterDirection.Input);
        parameters.Add("HolidayTypeId", request.HolidayTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("HolidayForShiftId", request.HolidayForShiftId, DbType.Int32, ParameterDirection.Input);

        var query = @"UpdateHoliday";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete

    public async Task DeleteHolidayAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteHoliday";
        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion
}
