namespace HumanResourcesWebApi.Models.Domain;

public class Certificate
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime GivenAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Organization { get; set; } 
    public DateTime? ValidUntil { get; set; } 
}
