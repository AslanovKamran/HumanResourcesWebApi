using HumanResourcesWebApi.Models.Requests.Countries;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;


namespace HumanResourcesWebApi.Repository.Dapper;

public class CountriesRepository(string connectionString) : ICountriesRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<Country>> GetCountriesAsync()
    {
        var query = "GetCountries";

        using (var db = new SqlConnection(_connectionString)) 
        {
            var result = await db.QueryAsync<Country>(query,commandType:CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddCountryAsync(AddCountryRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = "AddCountry";

        using (var db = new SqlConnection(_connectionString)) 
        {
            await db.ExecuteAsync(query,parameters, commandType:CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateCountryAsync(UpdateCountryRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);

        var query = "UpdateCountry";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteCountryAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteCountry";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
