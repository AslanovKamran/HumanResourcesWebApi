using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HumanResourcesWebApi.Models.Requests.Employees;

public class AddEmployeeRequest
{
    [Required]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "Surname can't be longer than 100 characters.")]
    public string Surname { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "Father's name can't be longer than 100 characters.")]
    public string FatherName { get; set; } = string.Empty;

    [Required]
    public int GenderId { get; set; }  // Assuming GenderId refers to an existing gender in the Genders table

    [Required]
    public int MaritalStatusId { get; set; }  // Assuming MaritalStatusId refers to an existing marital status

    [Required]
    public DateTime EntryDate { get; set; }  // Date of entry, required

    [Required]
    public int StateTableId { get; set; }  // Reference to the StateTables table

    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string? PhotoUrl { get; set; }  // Optional URL for the photo

    [JsonIgnore]
    public IFormFile? ImageFile { get; set; }
}


