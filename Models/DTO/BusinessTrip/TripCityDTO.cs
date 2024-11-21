namespace HumanResourcesWebApi.Models.DTO.BusinessTrip;


public class TripCityDTO
{
public int Id { get; set; }
public string Country { get; set; } = string.Empty;
public string City { get; set; } = string.Empty;
public string? DestinationPoint { get; set; }
}