using HumanResourcesWebApi.Models.Requests.Vacations;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IVacationsRepository
{
    Task<List<EmployeeVacation>> GetVacationsAsync(int employeeId, int? yearStarted = null, int? yearEnded = null);
    Task AddVacationAsync(AddVacationRequest request);
    Task UpdateVacationAsync(UpdateVacationRequest request);
    Task DeleteVacationAsync(int id);

}
