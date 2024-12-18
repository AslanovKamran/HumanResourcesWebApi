using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Reprimands;

public class AddReprimandRequest
{
   
    [DataType(DataType.Date)]
    public DateTime IssuedAt { get; set; }

    [DataType(DataType.Date)]
    public DateTime? TakenAt { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [StringLength(int.MaxValue)]
    public string? Reason { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "OrderNumber cannot exceed 255 characters.")]
    public string OrderNumber { get; set; } = string.Empty;

    [Required]
    public int TypeId { get; set; }

    [StringLength(255, ErrorMessage = "Amount cannot exceed 255 characters.")]
    public string? Amount { get; set; }
}
