namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

public class EmployeeFamilyMember
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
}
