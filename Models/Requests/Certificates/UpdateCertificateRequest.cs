using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Certificates;

public class UpdateCertificateRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? GivenAt { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(255, ErrorMessage = "The Name field cannot be null and exceed 255 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "The Organization field cannot exceed 255 characters.")]
    public string? Organization { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ValidUntil { get; set; }
}
