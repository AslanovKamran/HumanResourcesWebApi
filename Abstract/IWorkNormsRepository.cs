using HumanResourcesWebApi.Models.Requests.WorkNorms;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IWorkNormsRepository
{
    public Task<List<WorkNorm>> GetWorkNormsAsync(int? year);
    public Task AddWorkNormAsync(AddWorkNormRequest request);
    public Task UpdateWorkNormAsync(UpdateWorkNormRequest request);
    public Task DeleteWorkNormAsync(int year);
}
