using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Vacations;

namespace HumanResourcesWebApi.Abstract;

public interface IVacationsRepository
{
    Task<List<EmployeeVacation>> GetVacationsAsync(int employeeId, int? yearStarted = null, int? yearEnded = null);
    Task AddVacationAsync(AddVacationRequest request);
    Task UpdateVacationAsync(UpdateVacationRequest request);
    Task DeleteVacationAsync(int id);

}
