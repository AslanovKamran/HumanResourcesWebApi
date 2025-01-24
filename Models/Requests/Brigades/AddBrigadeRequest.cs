using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Brigades;

public class AddBrigadeRequest
{
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateTime FirstDate { get; set; }
    [Required]
    public DateTime SecondDate { get; set; }
    public int? WorkingHours { get; set; }
}
