namespace HumanResourcesWebApi.Models.Domain;

public class Substitute
{
    public int Id { get; set; }
    public int WhoId { get; set; }
    public int WhomId { get; set; }
    public string? Note { get; set; } = string.Empty;
    public int? TabelVacationId { get; set; }
    public string? InsertedUser { get; set; }
    public DateTime? InsertedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? SubStartDate { get; set; }
    public DateTime? SubEndDate { get; set; }
    public int? Bulletin{ get; set; }
}
