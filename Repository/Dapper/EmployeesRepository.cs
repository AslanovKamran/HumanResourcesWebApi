using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;

namespace HumanResourcesWebApi.Repository.Dapper;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly string _connectionString;

    public EmployeesRepository(string connectionString) => _connectionString = connectionString;

    public async Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(int itemsPerPage = 10, int currentPage = 1)
    {
        int skip = (currentPage - 1) * itemsPerPage;
        int take = itemsPerPage;

        using (var db = new SqlConnection(_connectionString))
        {

            var parameters = new DynamicParameters();
            parameters.Add("Skip", skip, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Take", take, DbType.Int32, ParameterDirection.Input);


            string query = @"exec GetEmployeesInChunks @Skip, @Take";

            // Execute the stored procedure using Dapper (or ADO.NET if you prefer)
            using (var multi = await db.QueryMultipleAsync(query, parameters))
            {
                // First result set: Get the total count of employees
                int totalCount = multi.Read<int>().Single();

                // Second result set: Get the actual employee data
                var employees = multi.Read<EmployeesChunk>().ToList();

                // Create the PageInfo object with the calculated properties
                var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                return (pageInfo, employees);
            }
        }
    }
}
