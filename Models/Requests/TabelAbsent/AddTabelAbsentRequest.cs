namespace HumanResourcesWebApi.Models.Requests.TabelAbsent;

public class AddTabelAbsentRequest
{
   
    public DateTime Date { get; set; }
    public string? Cause { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int? Bulletin { get; set; }
    public string? InsertedUser { get; set; }
}
