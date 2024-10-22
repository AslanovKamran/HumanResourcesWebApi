using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.WorkNorms;

public class UpdateWorkNormRequest
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Year is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive number.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Monthly Working Hours are required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Monthly Working Hours must be greater than 0.")]
    public int MonthlyWorkingHours { get; set; }
}
