using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.PreviousNames;

namespace HumanResourcesWebApi.Abstract;

public interface IPreviousNamesRepository
{
    Task<List<PreviousName>> GetPreviousNamesAsync(int employeeId);   
    Task AddPreviousNameAsync(AddPreviousNameRequest request);   
    Task UpdatePreviousNameAsync(UpdatePreviousNameRequest request);   
    Task DeletePreviousNameAsync(int id);   
}
