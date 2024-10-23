using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Cities;

public class AddCityRequest
{
    [Required]
    public int CountryId { get; set; }

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string CityName{ get; set; } = string.Empty;
    
}
