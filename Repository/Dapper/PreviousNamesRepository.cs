using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.PreviousNames;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class PreviousNamesRepository : IPreviousNamesRepository
    {
        private readonly string _connectionString;
        public PreviousNamesRepository(string connectionString) => _connectionString = connectionString;
        public async Task<List<PreviousName>> GetPreviousNamesAsync(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
            var query = "GetPreviousName";
            using (var db = new SqlConnection(_connectionString))
            {

                var result = await db.QueryAsync<PreviousName>(query, parameters, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }
        public async Task AddPreviousNameAsync(AddPreviousNameRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);
            parameters.Add("ChangedAt", request.ChangedAt, DbType.Date, ParameterDirection.Input);

            var query = "AddPreviousName";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }


        }
        public async Task UpdatePreviousNameAsync(UpdatePreviousNameRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);
            parameters.Add("ChangedAt", request.ChangedAt, DbType.Date, ParameterDirection.Input);

            var query = "UpdatePreviousName";

            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeletePreviousNameAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var db = new SqlConnection(_connectionString))
            {

                var query = "DeletePreviousName";
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
