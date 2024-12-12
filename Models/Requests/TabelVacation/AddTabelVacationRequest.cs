namespace HumanResourcesWebApi.Models.Requests.TabelVacation;

public class AddTabelVacationRequest
{
    public int Id { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MainDay { get; set; }
    public int EmployeeId { get; set; }
    public DateTime? RecalDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? InsertedUser { get; set; }
 
}
