using HumanResourcesWebApi.Models.Requests.PoliticalParties;
using HumanResourcesWebApi.Models.Requests.Employees;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IEmployeesRepository
{
    #region Get

    Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(EmployeeFilter filter, int itemsPerPage = 10, int currentPage = 1);
    Task<(PageInfo PageInfo, List<EmployeeGeneralInfoDto>)> GetEmployeesGenerealInfo(int itemsPerPage = 10, int currentPage = 1);
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

    //Return the old photoUrl in order to delete it when update is performed
    Task<string> UpdateEmployeePhotoAsync(int id, string newPhotoUrl);

    #endregion

    #region Delete

    Task DeleteEmployeeAsync(int id);

    #endregion

}
