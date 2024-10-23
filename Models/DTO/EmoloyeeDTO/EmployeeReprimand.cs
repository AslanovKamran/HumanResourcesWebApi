namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

public class EmployeeReprimand
{
    public int Id { get; set; }
    public DateTime IssuedAt { get; set; }
    public string? Reason { get; set; }
    public string? OrderNumber { get; set; }
    public string? Amount { get; set; }
    public string ReprimandType { get; set; } = string.Empty;
    public DateTime? TakenAt { get; set; }
    public int EmployeeId { get; set; }
}
