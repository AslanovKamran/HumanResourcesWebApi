using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Countries;

public class AddCountryRequest
{
    [Required(AllowEmptyStrings =false)]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}
