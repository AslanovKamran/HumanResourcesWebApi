using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.PreviousWorkingPlaces;

public class AddPreviousWorkingPlaceRequest
{
    [Required(AllowEmptyStrings =false)]
    public string OrganizationName { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings =false)]
    public string Position { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
