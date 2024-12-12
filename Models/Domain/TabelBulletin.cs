namespace HumanResourcesWebApi.Models.Domain;

public class TabelBulletin
{
    public int Id { get; set; }
    public string? Serial { get; set; }
    public string? Number { get; set; }
    public DateTime? Date{ get; set; }
    public DateTime InavlidityBeginDate { get; set; }
    public DateTime InvalidityEndDate { get; set; }
    public string? Note{ get; set; }
    public int EmployeeId{ get; set; }
    public bool InvalidityContinues{ get; set; }
    public string? InsertedUser { get; set; }
    public DateTime? InsertedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
