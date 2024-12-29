using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Rights;

public class AddRightRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}
