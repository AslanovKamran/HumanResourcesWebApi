using HumanResourcesWebApi.Models.Requests.VacationTypes;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class VacationTypesRepository(string connectionString) : IVacationTypesRepository
{

    private readonly string _connectionString = connectionString;

    #region Get

    public async Task<List<VacationTypeDTO>> GetVacationTypesAsync()
    {
        var query = @"GetVacationTypes";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<VacationTypeDTO>(query,commandType:CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddVacationAsync(AddVacationTypeRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("VacationPaymentTypeId", request.VacationPaymentTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"AddVacationType";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Update
    public async Task UpdateVacationAsync(UpdateVacationTypeRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("VacationPaymentTypeId", request.VacationPaymentTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = @"UpdateVacationType";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

    }

    #endregion

    #region Delete
    public async Task DeleteVacationAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteVacationType";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
