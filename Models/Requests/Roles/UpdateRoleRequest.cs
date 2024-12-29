using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Roles;

public class UpdateRoleRequest
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}
