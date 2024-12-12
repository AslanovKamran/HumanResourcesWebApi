namespace HumanResourcesWebApi.Models.Domain;

public class TabelAbsent
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Cause { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int? Bulletin{ get; set; }
    public string? InsertedUser{ get; set; }
    public DateTime? InsertedDate{ get; set; }
    public string? UpdatedUser{ get; set; }
    public DateTime? UpdatedDate{ get; set; }
}
