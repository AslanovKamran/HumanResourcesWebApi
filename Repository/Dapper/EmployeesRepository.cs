using HumanResourcesWebApi.Models.Requests.PoliticalParties;
using HumanResourcesWebApi.Models.Requests.Employees;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;


namespace HumanResourcesWebApi.Repository.Dapper;

public class EmployeesRepository(string connectionString) : IEmployeesRepository
{
    private readonly string _connectionString = connectionString;

    #region Get

    public async Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(EmployeeFilter filter, int itemsPerPage = 10, int currentPage = 1)
    {
        int skip = (currentPage - 1) * itemsPerPage;
        int take = itemsPerPage;

        using (var db = new SqlConnection(_connectionString))
        {

            var parameters = new DynamicParameters();

            parameters.Add("Skip", skip, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Take", take, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Surname", filter.Surname, DbType.String, ParameterDirection.Input);
            parameters.Add("Name", filter.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("FatherName", filter.FatherName, DbType.String, ParameterDirection.Input);
            parameters.Add("BirthDateStart", filter.BirthDateStart, DbType.Date, ParameterDirection.Input);
            parameters.Add("BirthDateEnd", filter.BirthDateEnd, DbType.Date, ParameterDirection.Input);
            parameters.Add("EntryDateStart", filter.EntryDateStart, DbType.Date, ParameterDirection.Input);
            parameters.Add("EntryDateEnd", filter.EntryDateEnd, DbType.Date, ParameterDirection.Input);
            parameters.Add("GenderId", filter.GenderId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("MaritalStatusId", filter.MaritalStatusId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("HasPoliticalParty", filter.HasPoliticalParty, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("HasSocialInsuranceNumber", filter.HasSocialInsuranceNumber, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("TabelNumber", filter.TabelNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("AnvisUserId", filter.AnvisUserId, DbType.String, ParameterDirection.Input);
            parameters.Add("IsWorking", filter.IsWorking, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("OrganizationFullName", filter.OrganizationFullName, DbType.String, ParameterDirection.Input);

            using (var multi = await db.QueryMultipleAsync("GetEmployeesInChunks", parameters, commandType: CommandType.StoredProcedure))
            {
                int totalCount = multi.Read<int>().Single();
                var employees = multi.Read<EmployeesChunk>().AsList();
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
            var query = "GetEmployeeGeneralInfo";
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

            var query = "GetPoliticalParty";
            var result = await db.QueryFirstOrDefaultAsync<EmployeeParty>(query, parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }

    public async Task<EmployeeMilitaryInfo> GetEmployeeMilitaryInfoAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EmployeeId", employeeId, DbType.Int32, ParameterDirection.Input);

        string query = "GetMilitaryInfo";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {

            var militaryInfo = await db.QuerySingleOrDefaultAsync<EmployeeMilitaryInfo>(
                query,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return militaryInfo!;
        }
    }

    #endregion

    #region Add

    public async Task AddEmployeeAsync(AddEmployeeRequest request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
           
            parameters.Add("Surname", request.Surname, DbType.String);
            parameters.Add("Name", request.Name, DbType.String);
            parameters.Add("FatherName", request.FatherName, DbType.String);
            parameters.Add("GenderId", request.GenderId, DbType.Int32);
            parameters.Add("MaritalStatusId", request.MaritalStatusId, DbType.Int32);
            parameters.Add("EntryDate", request.EntryDate, DbType.Date);
            parameters.Add("StateTableId", request.StateTableId, DbType.Int32);
            parameters.Add("PhotoUrl", request.PhotoUrl, DbType.String, size: 255);

            var query = @"AddEmployee";

            await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion

    #region Update

    public async Task UpdateEmployeeGeneralInfoAsync(UpdateEmployeeGeneralInfoRequest request)
    {
        using (var db = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id);
            parameters.Add("Surname", request.Surname);
            parameters.Add("Name", request.Name);
            parameters.Add("FatherName", request.FatherName);
            parameters.Add("PhotoUrl", request.PhotoUrl);
            parameters.Add("BirthDate", request.BirthDate);
            parameters.Add("BirthPlace", request.BirthPlace);
            parameters.Add("NationalityId", request.NationalityId);
            parameters.Add("GenderId", request.GenderId);
            parameters.Add("MaritalStatusId", request.MaritalStatusId);
            parameters.Add("SocialInsuranceNumber", request.SocialInsuranceNumber);
            parameters.Add("TabelNumber", request.TabelNumber);
            parameters.Add("AnvisUserId", request.AnvisUserId);
            parameters.Add("TrainershipYear", request.TrainershipYear);
            parameters.Add("TrainershipMonth", request.TrainershipMonth);
            parameters.Add("TrainershipDay", request.TrainershipDay);
            parameters.Add("RegistrationAddress", request.RegistrationAddress);
            parameters.Add("LivingAddress", request.LivingAddress);
            parameters.Add("MobileNumber", request.MobileNumber);
            parameters.Add("MobileNumber2", request.MobileNumber2);
            parameters.Add("MobileNumber3", request.MobileNumber3);
            parameters.Add("TelephoneNumber", request.TelephoneNumber);
            parameters.Add("InternalNumber", request.InternalNumber);
            parameters.Add("Email", request.Email);
            parameters.Add("IsTradeUnionMember", request.IsTradeUnionMember);
            parameters.Add("IsVeteran", request.IsVeteran);
            parameters.Add("HasWarInjury", request.HasWarInjury);
            parameters.Add("DisabilityDegree", request.DisabilityDegree);
            parameters.Add("HasDisabledChild", request.HasDisabledChild);
            parameters.Add("IsRefugeeFromAnotherCountry", request.IsRefugeeFromAnotherCountry);
            parameters.Add("IsRefugee", request.IsRefugee);

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

    public async Task UpdateMilitaryInfoAsync(EmployeeMilitaryInfo militaryInfo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", militaryInfo.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("TicketNumber", militaryInfo.TicketNumber, DbType.String, ParameterDirection.Input);
        parameters.Add("RegistrationGroup", militaryInfo.RegistrationGroup, DbType.String, ParameterDirection.Input);
        parameters.Add("RegistrationRate", militaryInfo.RegistrationRate, DbType.String, ParameterDirection.Input);
        parameters.Add("Content", militaryInfo.Content, DbType.String, ParameterDirection.Input);
        parameters.Add("Rank", militaryInfo.Rank, DbType.String, ParameterDirection.Input);
        parameters.Add("Period", militaryInfo.Period, DbType.String, ParameterDirection.Input);
        parameters.Add("Specialization", militaryInfo.Specialization, DbType.String, ParameterDirection.Input);
        parameters.Add("Number", militaryInfo.Number, DbType.String, ParameterDirection.Input);
        parameters.Add("Fitness", militaryInfo.Fitness, DbType.String, ParameterDirection.Input);
        parameters.Add("Commissariat", militaryInfo.Commissariat, DbType.String, ParameterDirection.Input);
        parameters.Add("SpecialAccountNumber", militaryInfo.SpecialAccountNumber, DbType.String, ParameterDirection.Input);

        string query = "UpdateMilitaryInfo";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {


            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<string> UpdateEmployeePhotoAsync(int id, string newPhotoUrl)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("NewPhotoUrl", newPhotoUrl, DbType.String, ParameterDirection.Input, size: 255); // Explicit size for input
        parameters.Add("OldPhotoUrl", dbType: DbType.String, direction: ParameterDirection.Output, size: 255); // Explicit size for output

        var query = "UpdateEmployeesPhotoUrl";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value
            return parameters.Get<string>("OldPhotoUrl");
        }
    }

    #endregion

    #region Delete

    public async Task DeleteEmployeeAsync(int id)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        var query = @"DeleteEmployee";

        using (var db = new SqlConnection(_connectionString))
        {
            await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    #endregion
}
