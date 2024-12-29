using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Users;

public class ChangePasswordRequest
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string NewPassword { get; set; } = string.Empty;
}
