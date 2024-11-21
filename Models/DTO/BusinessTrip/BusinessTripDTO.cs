namespace HumanResourcesWebApi.Models.DTO.BusinessTrip;


public class BusinessTripDTO
{
public int Id { get; set; }
public string? DocumentNumber { get; set; }
public DateTime? DocumentDate { get; set; }
public DateTime TripCardGivenAt { get; set; }
public string? TripCardNumber { get; set; }
public DateTime StartDate { get; set; }
public DateTime EndDate { get; set; }
public string Purpose { get; set; } = string.Empty;
public string? OrganizationInCharge { get; set; }
public string? Note { get; set; }
}