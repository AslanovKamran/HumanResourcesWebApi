namespace HumanResourcesWebApi.Models.Domain;

public class PreviousName
{ 
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ChangedAt { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Fathername { get; set; } = string.Empty;
}
