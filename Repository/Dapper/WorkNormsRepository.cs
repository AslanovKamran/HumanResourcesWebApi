using HumanResourcesWebApi.Models.Requests.WorkNorms;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class WorkNormsRepository(string connectionString) : IWorkNormsRepository
    {
        private readonly string _connectionString = connectionString;

        #region Get
        public async Task<List<WorkNormDTO>> GetWorkNormsAsync(int? year)
        {
            year ??= DateTime.Now.Year;

            var parameters = new DynamicParameters();
            parameters.Add("Year", year, DbType.Int32, ParameterDirection.Input);

            var query = @"GetWorkNorms";

            using (var db = new SqlConnection(_connectionString))
            {
                var result = await db.QueryAsync<WorkNormDTO>(query, parameters, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }

        #endregion

        #region Add
        public async Task AddWorkNormAsync(AddWorkNormRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Year", request.Year, DbType.Int32);
            parameters.Add("MonthId", request.MonthId, DbType.Int32);
            parameters.Add("MonthlyWorkingHours", request.MonthlyWorkingHours, DbType.Int32);

            var query = @"AddWorkNorms";

            using (var db = new SqlConnection(_connectionString)) 
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure); 
            }
        }
        #endregion

        #region Update
        public async Task UpdateWorkNormAsync(UpdateWorkNormRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32);
            parameters.Add("Year", request.Year, DbType.Int32);
            parameters.Add("MonthlyWorkingHours", request.MonthlyWorkingHours, DbType.Int32);

            var query = @"UpdateWorkNorm"; 

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region MyRegion
        public async Task DeleteWorkNormAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = @"DeleteWorkNorm";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

    }
}
