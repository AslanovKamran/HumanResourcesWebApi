using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Vacations;

public class AddVacationRequest
{
    
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "DaysWorking must be a non-negative integer.")]
    public int DaysWorking { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DaysTotal must be at least 1.")]
    public int DaysTotal { get; set; }

    [Required]
    public int YearStarted { get; set; }

    [Required]
    public int YearEnded { get; set; }

    [Required]
    public int VacationTypeId { get; set; }
}
