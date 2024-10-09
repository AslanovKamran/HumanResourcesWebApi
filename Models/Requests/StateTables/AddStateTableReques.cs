using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.StateTables;


public class AddStateTableRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Unit count must be greater than zero.")]
    public int UnitCount { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Monthly salary from cannot be negative.")]
    public int? MonthlySalaryFrom { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Monthly salary to cannot be negative.")]
    public int? MonthlySalaryTo { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Occupied post count cannot be negative.")]
    public int? OccupiedPostCount { get; set; }

    [MaxLength(100, ErrorMessage = "Document number cannot exceed 100 characters.")]
    public string? DocumentNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DocumentDate { get; set; }

    [Required(ErrorMessage = "Organization structure is required.")]
    public int OrganizationStructureId { get; set; }

    [Required(ErrorMessage = "Work type is required.")]
    public int WorkTypeId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Work hours cannot be negative.")]
    public int? WorkHours { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Work hours on Saturday cannot be negative.")]
    public int? WorkHoursSaturday { get; set; }

    public int? TabelPosition { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Tabel priority cannot be negative.")]
    public int? TabelPriority { get; set; }

    public int? ExcludeBankomat { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Degree cannot be negative.")]
    public int? Degree { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Hourly salary cannot be negative.")]
    public decimal? HourlySalary { get; set; }

    public bool IsCanceled { get; set; } = false;

    [MaxLength(100, ErrorMessage = "Working hours special field cannot exceed 100 characters.")]
    public string? WorkingHoursSpecial { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Monthly salary extra cannot be negative.")]
    public int? MonthlySalaryExtra { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Harmfulness coefficient cannot be negative.")]
    public int? HarmfulnessCoefficient { get; set; }
}


