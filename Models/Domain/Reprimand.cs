namespace HumanResourcesWebApi.Models.Domain;

public class Reprimand
{
    public int Id { get; set; }
    public DateTime IssuedAt{ get; set; }
    public DateTime TakenAt{ get; set; }
    public int EmployeeId{ get; set; }
    public string? Reason { get; set; }
    public string? OrderNumber { get; set; }
    public int TypeId { get; set; }
    public string? Amount { get; set; }

}
