namespace HumanResourcesWebApi.Models.Domain;

public class UpdateEducationRequest
{
    public int Id { get; set; }
    public string? Speciality{ get; set; }
    public string? Institution{ get; set; }
    public int EmployeeId{ get; set; }
    public int EducationTypeId{ get; set; }
    public int DiplomaTypeId { get; set; }
    public string? DiplomaNumber { get; set; }
    public int EducationKindId { get; set; }
    public DateTime EducationStartedAt { get; set; }
    public DateTime EducationEndedAt { get; set; }
}
