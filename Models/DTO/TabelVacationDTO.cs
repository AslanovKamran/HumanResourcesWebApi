namespace HumanResourcesWebApi.Models.DTO;

public class TabelVacationDTO
{
    public int Id { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MainDay { get; set; }
    public DateTime? RecalDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
  
}
