namespace HumanResourcesWebApi.Models.DTO;

public class BrigadeDto
{
    public int BrigadeId { get; set; }
    public string BrigadeName { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
}
