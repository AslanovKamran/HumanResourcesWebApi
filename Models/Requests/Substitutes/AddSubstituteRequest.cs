namespace HumanResourcesWebApi.Models.Requests.Substitutes;

public class AddSubstituteRequest
{
    public int Id { get; set; }
    public int WhoId { get; set; }
    public int WhomId { get; set; }
    public int? TabelVacationId { get; set; }
    public int? BulletinId { get; set; }
    public DateTime? SubStartDate { get; set; }
    public DateTime? SubEndDate { get; set; }
    public string? Note { get; set; } = string.Empty;
    public string? InsertedUser { get; set; }
}
