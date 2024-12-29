using HumanResourcesWebApi.Models.Requests.Rights;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface IRightsRepository
{
    Task AddRightAsync(AddRightRequest request);
    Task<List<Right>> GetRightsListAsync();
    Task<Right> GetRightByIdAsync(int id);
    Task UpdateRightByIdAsync(UpdateRightRequest request);
}
