using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.PreviousNames;

public class AddPreviousNameRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public DateTime ChangedAt { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
    public string Surname { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "FatherName cannot exceed 50 characters.")]
    public string FatherName { get; set; } = string.Empty;
}
