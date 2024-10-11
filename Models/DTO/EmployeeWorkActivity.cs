using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.DTO;

public class EmployeeWorkActivity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string WorkActivityType { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime WorkActivityDate { get; set; }

    [StringLength(50)]
    public string? OrderNumber { get; set; }

    [StringLength(255)]
    public string? WorkActivityReason { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [Required]
    [StringLength(255)]
    public string OrganizationStructureFullName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string StateTableName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? WorkShiftType { get; set; }

    [StringLength(50)]
    public string? WorkDayOffType { get; set; }

    [DataType(DataType.Date)]
    public DateTime? WorkShiftStartedAt { get; set; }
}
