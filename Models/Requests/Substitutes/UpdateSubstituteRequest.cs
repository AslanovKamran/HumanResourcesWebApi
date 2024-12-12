namespace HumanResourcesWebApi.Models.Requests.Substitutes;

public class UpdateSubstituteRequest
{
    public int Id { get; set; }
    public int WhoId { get; set; }
    public DateTime? SubStartDate { get; set; }
    public DateTime? SubEndDate { get; set; }
    public string? Note { get; set; } = string.Empty;
    public string? UpdatedUser { get; set; }
}
