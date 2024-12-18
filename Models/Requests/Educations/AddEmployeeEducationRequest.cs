using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Educations;

public class AddEmployeeEducationRequest
{
   

    [Required]
    public int EducationTypeId { get; set; }

    [StringLength(255)]
    public string? Institution { get; set; }

    [StringLength(255)]
    public string? Speciality { get; set; }

    [Required]
    public int EducationKindId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EducationStartedAt { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EducationEndedAt { get; set; }

    [Required]
    public int DiplomaTypeId { get; set; }

    [StringLength(255)]
    public string? DiplomaNumber { get; set; }

    [Required]
    public int EmployeeId { get; set; }
}
