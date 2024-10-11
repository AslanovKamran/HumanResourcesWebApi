namespace HumanResourcesWebApi.Models.Domain;
public class WorkActivity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string OrderNumber{ get; set; } = string.Empty;
    public DateTime OrderDate{ get; set; }
    public DateTime WorkActivityDate{ get; set; }
    public string WorkActivityReason { get; set; } = string.Empty;
    public int WorkActivityTypeId{ get; set; }
    public int NewStateTableId{ get; set; }
    public int WorkShiftTypeId{ get; set; }
    public DateTime WorkShiftStartedAt{ get; set; }
    public string Note{ get; set; } = string.Empty;
    public int WorkDayOffId{ get; set; }
    public string InsertedBy{ get; set; } = string.Empty;
    public DateTime InsertedAt{ get; set; }
    public string UpdatedBy{ get; set; } = string.Empty;
    public DateTime UpdatedAt{ get; set; }
}
