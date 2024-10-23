using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Cities;

public class UpdateCityRequest
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string CityName { get; set; } = string.Empty;

    [Required]
    public int CountryId { get; set; }
}
