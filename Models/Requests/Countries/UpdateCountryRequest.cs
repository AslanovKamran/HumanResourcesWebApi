using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Countries;

public class UpdateCountryRequest
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}
