namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

public class EmployeeVacation
{
    public int Id { get; set; }
    public int YearStarted { get; set; }
    public int? YearEnded { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public int DaysTotal { get; set; }
    public int DaysWorking { get; set; }
}
