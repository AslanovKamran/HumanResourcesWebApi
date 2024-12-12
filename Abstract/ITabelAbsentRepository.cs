using HumanResourcesWebApi.Models.Requests.TabelAbsent;
using HumanResourcesWebApi.Models.Domain;

namespace HumanResourcesWebApi.Abstract;

public interface ITabelAbsentRepository
{
    Task<List<TabelAbsent>> GetTabelAbsentsByIdAsync(int employeeId, int? year);
    Task AddTabelAbsentAsync(AddTabelAbsentRequest request);
    Task UpdateTabelAbsentAsync(UpdateTabelAbsentRequest request);
    Task DeleteTabelAbsentAsync(int id);
}
