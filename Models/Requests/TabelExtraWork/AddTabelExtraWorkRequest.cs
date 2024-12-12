namespace HumanResourcesWebApi.Models.Requests.TabelExtraWork;

public class AddTabelExtraWorkRequest
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public int EmployeeId { get; set; }
    public int ExtraWorkType { get; set; }
    public int? ExtraWorkHours { get; set; }
    public int? ExtraWorkNightHours { get; set; }
    public string? OrderNumber { get; set; }
    public string? Note { get; set; }
    public string? InsertedUser { get; set; }
}
