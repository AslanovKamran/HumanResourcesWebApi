namespace HumanResourcesWebApi.Models.DTO.BusinessTrip;


public class BusinessTripDetailsDTO
{
    public BusinessTripDTO BusinessTrip { get; set; } = new();
    public List<TripEmployeeDTO> Employees { get; set; } = new();
    public List<TripCityDTO> Cities { get; set; } = new();
}