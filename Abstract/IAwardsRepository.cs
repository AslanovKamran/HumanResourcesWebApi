using HumanResourcesWebApi.Models.Requests.Awards;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IAwardsRepository
{
    Task<List<EmloyeeAward>> GetAwardsAsync(int employeeId);

    Task AddAwardAsync(AddAwardRequest request);

    Task UpdateAwardAsync(UpdateAwardRequest request);

    Task DeleteAwardAsync(int id);
}
