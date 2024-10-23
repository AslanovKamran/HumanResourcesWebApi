using HumanResourcesWebApi.Models.Requests.FamilyMembers;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

namespace HumanResourcesWebApi.Repository.Dapper;

public class FamilyMembersRepository(string connectionString) : IFamilyMembersRepository
{
    private readonly string _connectionString = connectionString;

    #region Get
    public async Task<List<EmployeeFamilyMember>> GetFamilyMembersAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        var query = "GetFamilyMembers";

        using (var db = new SqlConnection(_connectionString))
        {
            var result = await db.QueryAsync<EmployeeFamilyMember>(query, parameters, commandType: CommandType.StoredProcedure);
            return result.AsList();
        }
    }

    #endregion

    #region Add
    public async Task AddFamilyMemberAsync(AddFamilyMemberRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("EmployeeId", request.EmployeeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("FamilyMemberTypeId", request.FamilyMemberTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BirthYear", request.BirthYear, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
        parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);

        var query = "AddFamilyMember";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update
    public async Task UpdateFamilyMemberAsync(UpdateFamilyMemberRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("FamilyMemberTypeId", request.FamilyMemberTypeId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("BirthYear", request.BirthYear, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Surname", request.Surname, DbType.String, ParameterDirection.Input);
        parameters.Add("FatherName", request.FatherName, DbType.String, ParameterDirection.Input);

        var query = "UpdateFamilyMember";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Delete
    public async Task DeleteFamilyMemberAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = "DeleteFamilyMember";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

}
