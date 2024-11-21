namespace HumanResourcesWebApi.Models.DTO.BusinessTrip;


public class TripEmployeeDTO
{
public int Id { get; set; }
public int EmployeeId { get; set; }
public string Name { get; set; }    = string.Empty;
public string Surname { get; set; } = string.Empty;
}