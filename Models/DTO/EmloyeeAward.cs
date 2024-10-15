namespace HumanResourcesWebApi.Models.DTO;

public class EmloyeeAward
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string AwardTypeDescription { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string? Amount { get; set; }
    public string? Note { get; set; }
    public int EmployeeId { get; set; }
}
