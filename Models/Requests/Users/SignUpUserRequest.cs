using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HumanResourcesWebApi.Models.Requests.Users;

public class SignUpUserRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public int? StructureId { get; set; }
    public string? InsertedBy { get; set; } = string.Empty;
    public bool CanEdit { get; set; }

    public string? Rights { get; set; } = string.Empty; // Comma-separated string of Rights

    [JsonIgnore] // Exclude from serialization
    [NotMapped]  // Exclude from ORM mapping
    internal string? Salt { get; set; } // Internal for programmatic initialization

}
