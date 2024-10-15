using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Awards;

namespace HumanResourcesWebApi.Abstract;

public interface IAwardsRepository
{
    // Retrieve awards by EmployeeId
    Task<List<EmloyeeAward>> GetAwardsAsync(int employeeId);

    // Add a new award
    Task AddAwardAsync(AddAwardRequest request);

    // Update an existing award
    Task UpdateAwardAsync(UpdateAwardRequest request);

    // Delete an award by Id
    Task DeleteAwardAsync(int id);
}
