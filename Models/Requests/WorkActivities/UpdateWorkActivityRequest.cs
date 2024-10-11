using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.WorkActivities;

public class UpdateWorkActivityRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int WorkActivityTypeId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime WorkActivityDate { get; set; }

    [StringLength(50)]
    public string? OrderNumber { get; set; }

    [StringLength(255)]
    public string? WorkActivityReason { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [Required]
    public int NewStateTableId { get; set; }

    public int? WorkShiftTypeId { get; set; }  // Nullable if this field is optional

    public int? WorkDayOffId { get; set; }      // Nullable if this field is optional

    [DataType(DataType.Date)]
    public DateTime? WorkShiftStartedAt { get; set; }
}
