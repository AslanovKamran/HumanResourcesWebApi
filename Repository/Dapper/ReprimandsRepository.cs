using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests.Reprimands;

namespace HumanResourcesWebApi.Repository.Dapper
{
    public class ReprimandsRepository : IReprimandsRepository
    {
        private readonly string _connectionString;
        public ReprimandsRepository(string connectionString) => _connectionString = connectionString;
        public async Task<List<EmployeeReprimand>> GetReprimandsAsync(int employeeId)
        {
            var parameters = new DynamicParameters();   
            parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

            var query = "GetReprimands";

            using (var db = new SqlConnection(_connectionString)) 
            {
                var result = (await db.QueryAsync<EmployeeReprimand>(query, parameters, commandType: CommandType.StoredProcedure));
                return result.AsList();
            }
        }
        public async Task AddReprimandAsync(AddReprimandRequest request)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IssuedAt", request.IssuedAt, DbType.Date, ParameterDirection.Input);
                parameters.Add("TakenAt", request.TakenAt ?? null, DbType.Date, ParameterDirection.Input);
                parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);
                parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);

                var query = "AddReprimand";
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateReprimandAsync(UpdateReprimandRequest request)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IssuedAt", request.IssuedAt, DbType.Date, ParameterDirection.Input);
                parameters.Add("TypeId", request.TypeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("OrderNumber", request.OrderNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("Amount", request.Amount, DbType.String, ParameterDirection.Input);
                parameters.Add("Reason", request.Reason, DbType.String, ParameterDirection.Input);
                parameters.Add("TakenAt", request.TakenAt ?? null, DbType.Date, ParameterDirection.Input);

                var query = "UpdateReprimand";
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteReprimandAsync(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

                var query = "DeleteReprimand";
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
