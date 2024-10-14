namespace HumanResourcesWebApi.Models.Domain;

public class Vacation
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int DaysWorking { get; set; }
    public int DaysTotal { get; set; }
    public int YearStarted { get; set; }
    public int YearsEnded { get; set; }
}
