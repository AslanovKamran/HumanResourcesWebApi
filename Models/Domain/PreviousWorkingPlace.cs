namespace HumanResourcesWebApi.Models.Domain;

public class PreviousWorkingPlace
{
    public int Id { get; set; }
    public string OrganizationName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public int EmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
