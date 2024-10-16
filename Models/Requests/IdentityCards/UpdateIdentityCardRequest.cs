using System.ComponentModel.DataAnnotations;
namespace HumanResourcesWebApi.Models.Requests.IdentityCards;


public class UpdateIdentityCardRequest
{
    [Required]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "Series cannot exceed 50 characters.")]
    public string Series { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "Card Number cannot exceed 100 characters.")]
    public string CardNumber { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "Organization cannot exceed 100 characters.")]
    public string Organization { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? GivenAt { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ValidUntil { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50, ErrorMessage = "FinCode cannot exceed 50 characters.")]
    public string FinCode { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "PhotoFront URL cannot exceed 255 characters.")]
    public string? PhotoFront { get; set; }

    [StringLength(255, ErrorMessage = "PhotoBack URL cannot exceed 255 characters.")]
    public string? PhotoBack { get; set; }
}
