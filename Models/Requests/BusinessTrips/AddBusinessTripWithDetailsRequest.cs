using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.BusinessTrips;

public class AddBusinessTripWithDetailsRequest
{
  
    [Required(AllowEmptyStrings = false)]
    [StringLength(255)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string EmployeeIds { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string CityIds { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string DestinationPoints { get; set; } = string.Empty;

    [StringLength(50)]
    public string? DocumentNumber { get; set; }

    public DateTime? DocumentDate { get; set; }

    [Required]
    public DateTime TripCardGivenAt { get; set; }

    [StringLength(50)]
    public string? TripCardNumber { get; set; }

    [StringLength(100)]
    public string? OrganizationInCharge { get; set; }

    [StringLength(300)]
    public string? Note { get; set; }
}
