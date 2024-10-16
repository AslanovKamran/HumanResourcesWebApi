using HumanResourcesWebApi.Models.Requests.Certificates;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class CertificatesRepositoty : ICertificatesRepository
{
    private readonly string _connectionString;

    public CertificatesRepositoty(string connectionString) => _connectionString = connectionString;

    public async Task AddCertificateAsync(AddCertificateRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("GivenAt", request.GivenAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Organization", request.Organization, DbType.String, ParameterDirection.Input);
        parameters.Add("ValidUntil", request.ValidUntil, DbType.Date, ParameterDirection.Input);

        var query = @"AddCertificate";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<List<Certificate>> GetEmployeesCertificates(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);
        var query = @"GetEmployeesCerificates";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = (await db.QueryAsync<Certificate>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
            return result;
        }
    }

    public async Task UpdateCertificateAsync(UpdateCertificateRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("GivenAt", request.GivenAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Organization", request.Organization, DbType.String, ParameterDirection.Input);
        parameters.Add("ValidUntil", request.ValidUntil, DbType.Date, ParameterDirection.Input);

        var query = "UpdateCertificate";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task DeleteCertificateAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteCertificate";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
