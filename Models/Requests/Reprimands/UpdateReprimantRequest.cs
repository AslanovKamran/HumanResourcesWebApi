using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Reprimands;

public class UpdateReprimandRequest
{
    [Required]
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime IssuedAt { get; set; }

    [Required]
    public int TypeId { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "OrderNumber cannot exceed 255 characters.")]
    public string OrderNumber { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "Amount cannot exceed 255 characters.")]
    public string? Amount { get; set; }

    public string? Reason { get; set; }

    [DataType(DataType.Date)]
    public DateTime? TakenAt { get; set; }
}

