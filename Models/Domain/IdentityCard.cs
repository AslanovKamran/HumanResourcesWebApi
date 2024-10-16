namespace HumanResourcesWebApi.Models.Domain;

public class IdentityCard
{
    public int Id { get; set; }
    public string Series { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public DateTime GivenAt { get; set; } 
    public string FinCode { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public DateTime ValidUntil{ get; set; } 
    public string? PhotoFront{ get; set; } 
    public string? PhotoBack { get; set; } 
    public int EmployeeId { get; set; }
}
