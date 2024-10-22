using HumanResourcesWebApi.Models.Requests.VacationTypes;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Abstract;

public interface IVacationTypesRepository
{
    Task<List<VacationType>> GetVacationTypesAsync();

    Task AddVacationAsync(AddVacationTypeRequest request);
    Task UpdateVacationAsync(UpdateVacationTypeRequest request);
    Task DeleteVacationAsync(int id);
}
