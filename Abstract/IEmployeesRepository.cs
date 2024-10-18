using HumanResourcesWebApi.Models.Requests.PoliticalParties;
using HumanResourcesWebApi.Models.Requests.Employees;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IEmployeesRepository
{
    #region Get

    Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(EmployeeFilter filter, int itemsPerPage = 10, int currentPage = 1);
    Task<EmployeeGeneralInfoDto> GetEmployeeGeneralInfoAsync(int id);
    Task<EmployeeParty> GetPoliticalPartyAsync(int id);
    Task<EmployeeMilitaryInfo> GetEmployeeMilitaryInfoAsync(int id);

    #endregion

    #region Add
    Task AddEmployeeAsync(AddEmployeeRequest request);

    #endregion

    #region Update
    Task UpdateEmployeeGeneralInfoAsync(UpdateEmployeeGeneralInfoRequest request);
    Task UpdatePoliticalPartyAsync(UpdatePoliticalPartyRequest request);
    Task UpdateMilitaryInfoAsync(EmployeeMilitaryInfo militaryInfo);

    #endregion

    #region Delete

    Task DeleteEmployeeAsync(int id);
    
    #endregion

}
