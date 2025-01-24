namespace HumanResourcesWebApi.Models.Domain;

public class Brigade
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime FirstDate { get; set; }
    public DateTime SecondDate { get; set; }
    public int? WorkingHours { get; set; }
}
