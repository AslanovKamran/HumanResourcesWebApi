namespace HumanResourcesWebApi.Models.Domain;

public class Award
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int  EmployeeId { get; set; }
    public string? Note { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string? Amount { get; set; } 
}
