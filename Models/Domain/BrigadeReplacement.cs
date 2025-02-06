namespace HumanResourcesWebApi.Models.Domain;

public class BrigadeReplacement
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int FirstBrigadeId { get; set; }
    public int SecondBrigadeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
