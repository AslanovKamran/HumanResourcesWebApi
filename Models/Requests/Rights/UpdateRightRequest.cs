using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Rights;

public class UpdateRightRequest
{
    [Key]
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}
