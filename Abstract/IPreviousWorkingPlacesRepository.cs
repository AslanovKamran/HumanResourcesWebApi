using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.PreviousWorkingPlaces;

namespace HumanResourcesWebApi.Abstract;

public interface IPreviousWorkingPlacesRepository
{
    Task<List<PreviousWorkingPlace>> GetPreviousWorkingPlacesByEmployeeIdAsync(int employeeId);
    Task AddPreviousWorkingPlaceAsync(AddPreviousWorkingPlaceRequest request);
    Task UpdatePreviousWorkingPlaceAsync(UpdatePreviousWorkingPlaceRequest request);
    Task DeletePreviousWorkingPlaceAsync(int id);
}
