using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests.Certificates;
using HumanResourcesWebApi.Models.Requests.Cities;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class CitiesRepository(string connectionString) : ICitiesRepository
    {
        private readonly string _connectionString = connectionString;
        
        #region Get
        public async Task<(List<CityDTO> cities, PageInfo pageInfo)> GetCitiesAsync(int itemsPerPage = 50, int currentPage = 1)
        {
            int skip = (currentPage - 1) * itemsPerPage;
            int take = itemsPerPage;

            var parameters = new DynamicParameters();
            parameters.Add("Skip", skip, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Take", take, DbType.Int32, ParameterDirection.Input);

            var query = @"GetCities";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = await db.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure))
                {
                    int totalCount = multi.ReadSingle<int>();

                    var cities = multi.Read<CityDTO>();

                    var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                    return (cities.AsList(), pageInfo);
                }
            }
        }

        
        #endregion

        #region Add
        public async Task AddCityAsync(AddCityRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", request.CountryId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CityName", request.CityName, DbType.String, ParameterDirection.Input);

            var query = "AddCity";

            using (var db = new SqlConnection(_connectionString)) 
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Update
        public async Task UpdateCityAsync(UpdateCityRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CountryId", request.CountryId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CityName", request.CityName, DbType.String, ParameterDirection.Input);

            var query = "UpdateCity";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Delete
        public async Task DeleteCityAsync(int id) 
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = @"DeleteCity";

            using (var db = new SqlConnection(_connectionString)) 
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }

        }
        #endregion
    }
}
