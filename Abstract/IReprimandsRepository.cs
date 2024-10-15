using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Reprimands;

namespace HumanResourcesWebApi.Abstract;

public interface IReprimandsRepository
{
    Task<List<EmployeeReprimand>> GetReprimandsAsync(int employeeId);
    Task AddReprimandAsync(AddReprimandRequest request);
    Task UpdateReprimandAsync(UpdateReprimandRequest request);
    Task DeleteReprimandAsync(int id);
}
