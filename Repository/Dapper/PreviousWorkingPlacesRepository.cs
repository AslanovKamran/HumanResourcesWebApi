using Azure.Core;
using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.PreviousWorkingPlaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper;

public class PreviousWorkingPlacesRepository : IPreviousWorkingPlacesRepository
{
    private readonly string _connectionString;
    public PreviousWorkingPlacesRepository(string connectionString) => _connectionString = connectionString;


    #region Get

    public async Task<List<PreviousWorkingPlace>> GetPreviousWorkingPlacesByEmployeeIdAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = @"GetPreviousWorkPlacesByEmployeeId";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<PreviousWorkingPlace>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }

    #endregion

    #region Add


    public async Task AddPreviousWorkingPlaceAsync(AddPreviousWorkingPlaceRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrganizationName", request.OrganizationName, DbType.String, ParameterDirection.Input);
        parameters.Add("Position", request.Position, DbType.String, ParameterDirection.Input);
        parameters.Add("StartedAt", request.StartedAt, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EndedAt", request.EndedAt, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);

        var query = @"AddPreviousWorkingPlace";


        using (var db = new SqlConnection(_connectionString))
        {
           await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
           
        }
    }


    #endregion

    #region Update
    public  async Task UpdatePreviousWorkingPlaceAsync(UpdatePreviousWorkingPlaceRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("OrganizationName", request.OrganizationName, DbType.String, ParameterDirection.Input);
        parameters.Add("Position", request.Position, DbType.String, ParameterDirection.Input);
        parameters.Add("StartedAt", request.StartedAt, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EndedAt", request.EndedAt, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);

        var query = @"UpdatePreviousWorkingPlace";


        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

        }

    }

    #endregion

    #region Delete

    public async Task DeletePreviousWorkingPlaceAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeletePreviousWorkingPlace";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

        }
    }

    #endregion



}
