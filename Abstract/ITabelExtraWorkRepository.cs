using HumanResourcesWebApi.Models.Requests.TabelExtraWork;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface ITabelExtraWorkRepository
{
    Task<List<TabelExtraWorkDTO>> GetExtraWorkAsync(int employeeId, int year);
    Task AddExtraWorkAsync(AddTabelExtraWorkRequest request);
    Task UpdateExtraWorkAsync(UpdateTabelExtraWorkRequest request);
    Task DeleteExtraWorkByIdAsync(int id);
}
