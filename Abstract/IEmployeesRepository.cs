using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IEmployeesRepository
{
    Task<(PageInfo PageInfo, List<EmployeesChunk> Employees)> GetEmployeesChunkAsync(int itemsPerPage = 10, int currentPage = 1); 
}
