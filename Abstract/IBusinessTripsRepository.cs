using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO.BusinessTrip;
using HumanResourcesWebApi.Models.Requests.BusinessTrips;

namespace HumanResourcesWebApi.Abstract;

public interface IBusinessTripsRepository
{
    Task AddBusinessTripWithDetailsAsync(AddBusinessTripWithDetailsRequest request);
    Task<BusinessTripDetailsDTO> GetBusinessTripDetailsAsync(int tripId);
    Task<(PageInfo PageInfo, List<BusinessTripDTO> BusinessTrips)> GetBusinessTrips(int itemsPerPage = 10, int currentPage = 1);
}
