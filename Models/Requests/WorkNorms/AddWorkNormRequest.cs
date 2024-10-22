using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.WorkNorms;

public class AddWorkNormRequest
{
    [Required(ErrorMessage = "Year is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive number.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "MonthId is required.")]
    [Range(1, 12, ErrorMessage = "MonthId must be between 1 and 12.")]
    public int MonthId { get; set; }

    [Required(ErrorMessage = "Monthly Working Hours are required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Monthly Working Hours must be greater than 0.")]
    public int MonthlyWorkingHours { get; set; }
}
