using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests;

public class AddOrganizationStructureRequest
{
    [MaxLength(10)]  // Code can be up to 10 characters
    public string? Code { get; set; }

    [Required(ErrorMessage = "Name is required")]  // Name is required
    [MaxLength(100)]  // Maximum length of 100 characters
    public required string Name { get; set; }

    [DataType(DataType.Date)]  // Ensure this is a valid date
    public DateTime? BeginningHistory { get; set; }

    public int? ParentId { get; set; }

    public string? FirstNumber { get; set; }

    public string? SecondNumber { get; set; }
}
