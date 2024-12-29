using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Users
{
    public class LoginUserRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; } = string.Empty;
    }
}
