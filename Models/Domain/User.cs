namespace HumanResourcesWebApi.Models.Domain;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    public int RoleId { get; set; }  // Role name from the joined table
    public string? Role { get; set; } = string.Empty; // Role name from the joined table
    public string? Structure { get; set; } = string.Empty; // Structure name from the joined table
    public string? InsertedBy { get; set; } = string.Empty;
    public DateTime? InsertedAt { get; set; }
    public string? UpdatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? PasswordUpdatedBy { get; set; } = string.Empty;
    public DateTime? PasswordUpdatedAt { get; set; }
    public bool CanEdit { get; set; }
    public List<Right>? Rights { get; set; } = new();
}
