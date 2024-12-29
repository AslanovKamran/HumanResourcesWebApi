using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Roles;

public class AddRoleRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}
