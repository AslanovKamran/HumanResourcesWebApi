using HumanResourcesWebApi.Models.Requests.GeneralInformation;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class GeneralInformationRepository(string connectionString) : IGeneralInformationRepository
    {
        private readonly string _connectionString = connectionString;

        #region Get

        public async Task<List<GeneralInformation>> GetGeneralInformationAsync()
        {
            var query = @"GetGeneralInformation";

            using (var db = new SqlConnection(_connectionString)) 
            {
                var result = await db.QueryAsync<GeneralInformation>(query, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }

        #endregion

        #region Update

        public async Task UpdateGeneralInformationAsync(UpdateGeneralInformationRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Text", request.Text, DbType.String, ParameterDirection.Input);

            var query = "UpdateGeneralInformation";

            using (var db = new SqlConnection(_connectionString)) 
            {
                await db.ExecuteAsync(query,parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}
