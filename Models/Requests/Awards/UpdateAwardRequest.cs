using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Awards;

public class UpdateAwardRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [StringLength(1000, ErrorMessage = "Note cannot exceed 1000 characters.")]
    public string? Note { get; set; }

    [Required]
    public int TypeId { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "OrderNumber cannot exceed 255 characters.")]
    public string OrderNumber { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "Amount cannot exceed 255 characters.")]
    public string? Amount { get; set; }
}
