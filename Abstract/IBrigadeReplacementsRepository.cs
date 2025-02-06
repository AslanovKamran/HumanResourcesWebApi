using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.BrigadeReplacements;

namespace HumanResourcesWebApi.Abstract;

public interface IBrigadeReplacementsRepository
{
    Task<List<BrigadeReplacement>> GetBrigadeReplacementsAsync();
    Task AddBrigadeReplacementAsync(AddBrigadeReplacementRequest request);
    Task DeleteBrigadeReplacementAsync(int id);
}
