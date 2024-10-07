using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IEmployeesRepository
{
    Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(EmployeeFilter filter, int itemsPerPage = 10, int currentPage = 1);
    Task AddEmployeeAsync(AddEmployeeRequest request);
    Task<EmployeeGeneralInfoDto> GetEmployeeGeneralInfoAsync(int id);
    Task UpdateEmployeeGeneralInfoAsync(UpdateEmployeeGeneralInfoRequest request);
}
