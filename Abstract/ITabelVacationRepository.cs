using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.TabelVacation;

namespace HumanResourcesWebApi.Abstract;

public interface ITabelVacationRepository
{
    Task<List<TabelVacationDTO>> GetTabelVacationsAsync(int employeeId, int? beginYear, int? endYear);
    Task AddTabelVacationAsync(AddTabelVacationRequest request);
    Task UpdateTabelVacationAsync(UpdateTabelVacationRequest request);
    Task DeleteTabelVacationAsync(int id);
}
