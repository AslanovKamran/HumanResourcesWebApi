using HumanResourcesWebApi.Models.DTO.BusinessTrip;
using HumanResourcesWebApi.Models.Requests.BusinessTrips;

namespace HumanResourcesWebApi.Abstract;

public interface IBusinessTripsRepository
{
    Task AddBusinessTripWithDetailsAsync(AddBusinessTripWithDetailsRequest request);
    Task<BusinessTripDetailsDTO> GetBusinessTripDetailsAsync(int tripId);
    Task<List<BusinessTripDTO>> GetBusinessTrips();
}
