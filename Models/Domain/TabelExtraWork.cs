namespace HumanResourcesWebApi.Models.Domain;

public class TabelExtraWork
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public int EmployeeId { get; set; }
    public int? ExtraWorkHours{ get; set; }
    public string? InsertedUser { get; set; }
    public DateTime? InsertedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? OrderNumber { get; set; }
    public int ExtraWorkType { get; set; }
    public string? Note { get; set; }
    public int ExtraWorkNightHours { get; set; }
}
