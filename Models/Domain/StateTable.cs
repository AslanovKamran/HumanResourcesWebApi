namespace HumanResourcesWebApi.Models.Domain;

public class StateTable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitCount { get; set; }
    public int? MonthlySalaryFrom { get; set; }
    public int? MonthlySalaryTo { get; set; }
    public int? OccupiedPostCount{ get; set; }
    public string? DocumentNumber{ get; set; }
    public DateTime? DocumentDate{ get; set; }
    public int OrganizationStructureId{ get; set; }
    public int WorkTypeId{ get; set; }
    public int? WorkHours{ get; set; }
    public int? WorkHoursSaturday{ get; set; }
    public int? TabelPosition{ get; set; }
    public int? TabelPriority{ get; set; }
    public int? ExcludeBankomat{ get; set; }
    public int? Degree{ get; set; }
    public decimal? HourlySalary{ get; set; }
    public bool IsCanceled{ get; set; }
    public string? WorkingHoursSpecial{ get; set; }
    public int? MonthlySalaryExtra{ get; set; }
    public int? HarmfulnessCoefficient{ get; set; }

    public OrganizationStructure OrganizationStructure { get; set; } = new();
    public StateWorkType StateWorkType{ get; set; } = new();

}
