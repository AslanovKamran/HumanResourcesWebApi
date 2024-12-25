namespace HumanResourcesWebApi.Models.Domain.TabelModels;

public class TabelModel
{
    public int? Id { get; set; } //EmployeeId
    public string TabelNumber { get; set; } = string.Empty;
    public string FinCode { get; set; } = string.Empty;
    public string SocialInsuranceNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty; //Full name of the the employee <Surname Name FatherName>
    public string StateTableName { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public int? Degree { get; set; }
    public string WorkType{ get; set; } = string.Empty;
    public int? WorkHours{ get; set; } 
    public int? WorkHoursSaturday{ get; set; }

    //Navigation properties -> should be deserialized to get the data from JSON
    public List<TabelModelBusinessTrips> BusinessTrips { get; set; } = new();
    public List<TabelModelExtraWork> ExtraWork { get; set; } = new();
    public List<TabelModelBulletin> Invalidity { get; set; } = new();
    public List<TabelModelAbsence> Absences { get; set; } = new();
    public List<TabelModelVacation> Vacations { get; set; } = new();
}
