namespace HumanResourcesWebApi.Models.Requests;

public class AddOrganizationStructureRequest
{
    public string? Code { get; set; }
    public required string Name{ get; set; }
    public DateTime? BeginningHistory{ get; set; }
    public int? ParentId{ get; set; }
    public required string FirstNumber{ get; set; }
    public required string SecondNumber{ get; set; }
}
