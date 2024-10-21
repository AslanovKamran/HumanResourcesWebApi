namespace HumanResourcesWebApi.Models.Domain;

public class Holiday
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public int HolidayTypeId { get; set; }
    public int? HolidayForShiftId { get; set; }
}
