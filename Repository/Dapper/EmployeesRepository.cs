using HumanResourcesWebApi.Models.Requests.PoliticalParties;
using HumanResourcesWebApi.Models.Requests.Employees;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace HumanResourcesWebApi.Repository.Dapper;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly string _connectionString;

    public EmployeesRepository(string connectionString) => _connectionString = connectionString;

    #region Get

    public async Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(EmployeeFilter filter, int itemsPerPage = 10, int currentPage = 1)
    {
        int skip = (currentPage - 1) * itemsPerPage;
        int take = itemsPerPage;

        using (var db = new SqlConnection(_connectionString))
        {

            var parameters = new DynamicParameters();

            parameters.Add("@Skip", skip, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Take", take, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Surname", filter.Surname ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);
            parameters.Add("@Name", filter.Name ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);
            parameters.Add("@FatherName", filter.FatherName ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);
            parameters.Add("@BirthDateStart", filter.BirthDateStart ?? (object)DBNull.Value, DbType.Date, ParameterDirection.Input);
            parameters.Add("@BirthDateEnd", filter.BirthDateEnd ?? (object)DBNull.Value, DbType.Date, ParameterDirection.Input);
            parameters.Add("@EntryDateStart", filter.EntryDateStart ?? (object)DBNull.Value, DbType.Date, ParameterDirection.Input);
            parameters.Add("@EntryDateEnd", filter.EntryDateEnd ?? (object)DBNull.Value, DbType.Date, ParameterDirection.Input);
            parameters.Add("@GenderId", filter.GenderId ?? (object)DBNull.Value, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@MaritalStatusId", filter.MaritalStatusId ?? (object)DBNull.Value, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@HasPoliticalParty", filter.HasPoliticalParty ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@HasSocialInsuranceNumber", filter.HasSocialInsuranceNumber ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@TabelNumber", filter.TabelNumber ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);
            parameters.Add("@AnvisUserId", filter.AnvisUserId ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsWorking", filter.IsWorking ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@OrganizationFullName", filter.OrganizationFullName ?? (object)DBNull.Value, DbType.String, ParameterDirection.Input);

            using (var multi = await db.QueryMultipleAsync("dbo.GetEmployeesInChunks", parameters, commandType: CommandType.StoredProcedure))
            {
                int totalCount = multi.Read<int>().Single();
                var employees = multi.Read<EmployeesChunk>().ToList();
                var pageInfo = new PageInfo(totalCount, itemsPerPage, currentPage);

                return (pageInfo, employees);
            }
        }
    }
    public async Task<EmployeeGeneralInfoDto> GetEmployeeGeneralInfoAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        using (var db = new SqlConnection(_connectionString))
        {
            var query = "dbo.GetUserGeneralInfo";
            var employee = await db.QueryFirstOrDefaultAsync<EmployeeGeneralInfoDto>(query, parameters, commandType: CommandType.StoredProcedure);
            return employee!;
        }
    }

    public async Task<EmployeeParty> GetPoliticalPartyAsync(int id)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            var query = "dbo.GetPoliticalParty";
            var result = await db.QueryFirstOrDefaultAsync<EmployeeParty>(query, parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }
    #endregion

    #region Add

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

            var query = @"AddEmployee";
                                        
            await connection.ExecuteAsync(query, parameters, commandType:CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update

    public async Task UpdateEmployeeGeneralInfoAsync(UpdateEmployeeGeneralInfoRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", request.Id);
            parameters.Add("@Surname", request.Surname);
            parameters.Add("@Name", request.Name);
            parameters.Add("@FatherName", request.FatherName);
            parameters.Add("@PhotoUrl", request.PhotoUrl);
            parameters.Add("@BirthDate", request.BirthDate);
            parameters.Add("@BirthPlace", request.BirthPlace);
            parameters.Add("@NationalityId", request.NationalityId);
            parameters.Add("@GenderId", request.GenderId);
            parameters.Add("@MaritalStatusId", request.MaritalStatusId);
            parameters.Add("@SocialInsuranceNumber", request.SocialInsuranceNumber);
            parameters.Add("@TabelNumber", request.TabelNumber);
            parameters.Add("@AnvisUserId", request.AnvisUserId);
            parameters.Add("@TrainershipYear", request.TrainershipYear);
            parameters.Add("@TrainershipMonth", request.TrainershipMonth);
            parameters.Add("@TrainershipDay", request.TrainershipDay);
            parameters.Add("@RegistrationAddress", request.RegistrationAddress);
            parameters.Add("@LivingAddress", request.LivingAddress);
            parameters.Add("@MobileNumber", request.MobileNumber);
            parameters.Add("@MobileNumber2", request.MobileNumber2);
            parameters.Add("@MobileNumber3", request.MobileNumber3);
            parameters.Add("@TelephoneNumber", request.TelephoneNumber);
            parameters.Add("@InternalNumber", request.InternalNumber);
            parameters.Add("@Email", request.Email);
            parameters.Add("@IsTradeUnionMember", request.IsTradeUnionMember);
            parameters.Add("@IsVeteran", request.IsVeteran);
            parameters.Add("@HasWarInjury", request.HasWarInjury);
            parameters.Add("@DisabilityDegree", request.DisabilityDegree);
            parameters.Add("@HasDisabledChild", request.HasDisabledChild);
            parameters.Add("@IsRefugeeFromAnotherCountry", request.IsRefugeeFromAnotherCountry);
            parameters.Add("@IsRefugee", request.IsRefugee);

            await db.ExecuteAsync("UpdateEmployeeGeneralInfo", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UpdatePoliticalPartyAsync(UpdatePoliticalPartyRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PoliticalParty", request.PoliticalParty, DbType.String, ParameterDirection.Input);
            parameters.Add("PartyMembershipNumber", request.PartyMembershipNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("PartyEntranceDate", request.PartyEntranceDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("PartyCardGivenDate", request.PartyCardGivenDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("PartyOrganizationRegion", request.PartyOrganizationRegion, DbType.String, ParameterDirection.Input);

            var query = "UpdatePoliticalParty";
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    #endregion

    #region Delete

    public async Task DeleteEmployeeAsync(int id) 
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id",id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteEmployee";

        using (var db = new SqlConnection(_connectionString)) 
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    

    #endregion
}
