using System.ComponentModel.DataAnnotations;


namespace HumanResourcesWebApi.Models.Requests.Medals;
public class AddMedalRequest
{
  

    [DataType(DataType.Date)]
    public DateTime? OrderDate { get; set; }

    [StringLength(100, ErrorMessage = "OrderNumber cannot exceed 100 characters.")]
    public string? OrderNumber { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
    public string? Note { get; set; }

    [Required]
    public int MedalTypeId { get; set; }
}
