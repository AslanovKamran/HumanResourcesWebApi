namespace HumanResourcesWebApi.Models.Domain;

public class FamilyMember
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int FamilyMemberTypeId { get; set; }
    public int BirthYear { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
}
