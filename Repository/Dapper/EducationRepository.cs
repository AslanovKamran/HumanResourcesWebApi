using HumanResourcesWebApi.Models.Requests.Educations;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

namespace HumanResourcesWebApi.Repository.Dapper;

public class EducationRepository(string connectionString) : IEducationRepository
{
    private readonly string _connectionString = connectionString;

    #region Get

    public async Task<List<EmployeeEducation>> GetEmployeeEducationAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            var query = "GetEmployeesEducation";
            var result = (await db.QueryAsync<EmployeeEducation>(query, parameters, commandType: CommandType.StoredProcedure));
            return result.AsList();
        }
    }
    #endregion

    #region Add
    public async Task AddEmployeeEducationAsync(AddEmployeeEducationRequest education)
    {
        var parameters = new DynamicParameters();

        parameters.Add("EducationTypeId", education.EducationTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Institution", education.Institution, DbType.String, ParameterDirection.Input);
        parameters.Add("Speciality", education.Speciality, DbType.String, ParameterDirection.Input);
        parameters.Add("EducationKindId", education.EducationKindId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EducationStartedAt", education.EducationStartedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("EducationEndedAt", education.EducationEndedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("DiplomaTypeId", education.DiplomaTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DiplomaNumber", education.DiplomaNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("EmployeeId", education.EmployeeId, DbType.Int32, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            var query = "AddEmloyeesEducation";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateEmployeeEducationAsync(UpdateEmployeeEducationRequest education)
    {
        var parameters = new DynamicParameters();


        parameters.Add("Id", education.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EducationTypeId", education.EducationTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Institution", education.Institution, DbType.String, ParameterDirection.Input);
        parameters.Add("Speciality", education.Speciality, DbType.String, ParameterDirection.Input);
        parameters.Add("EducationKindId", education.EducationKindId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EducationStartedAt", education.EducationStartedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("EducationEndedAt", education.EducationEndedAt, DbType.Date, ParameterDirection.Input);
        parameters.Add("DiplomaTypeId", education.DiplomaTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("DiplomaNumber", education.DiplomaNumber, DbType.String, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            var query = "UpdateEmployeesEducation";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteEmployeeEducationAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            var query = "DeleteEmployeesEducation";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
