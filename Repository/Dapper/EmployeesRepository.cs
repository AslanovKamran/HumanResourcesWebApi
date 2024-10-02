using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.Requests;

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

            using (var multi = await db.QueryMultipleAsync(query, parameters))
            {
                // First result set: Get the total count of employees
                int totalCount = multi.Read<int>().Single();

                // Second result set: Get the actual employee data
                var employees = multi.Read<EmployeesChunk>().ToList();


                var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                return (pageInfo, employees);
            }
        }
    }


    public async Task AddEmployeeAsync(AddEmployeeRequest request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", request.Id, DbType.Int32);
            parameters.Add("@Surname", request.Surname, DbType.String);
            parameters.Add("@Name", request.Name, DbType.String);
            parameters.Add("@FatherName", request.FatherName, DbType.String);
            parameters.Add("@GenderId", request.GenderId, DbType.Int32);
            parameters.Add("@MaritalStatusId", request.MaritalStatusId, DbType.Int32);
            parameters.Add("@EntryDate", request.EntryDate, DbType.Date);
            parameters.Add("@StateTableId", request.StateTableId, DbType.Int32);
            parameters.Add("@PhotoUrl", request.PhotoUrl, DbType.String, size: 255);

            var query = @"exec AddEmployee
                                             @Id, 
                                             @Surname, 
                                             @Name, 
                                             @FatherName, 
                                             @GenderId, 
                                             @MaritalStatusId, 
                                             @EntryDate, 
                                             @StateTableId, 
                                             @PhotoUrl";
                                            

            await connection.ExecuteAsync(query, parameters);
        }
    }

}
