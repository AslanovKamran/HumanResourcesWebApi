namespace HumanResourcesWebApi.Models.Requests.TabelAbsent;

public class UpdateTabelAbsentRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Cause{ get; set; }
    public int? Bulletin{ get; set; }
    public string? UpdatedUser{ get; set; }
}
