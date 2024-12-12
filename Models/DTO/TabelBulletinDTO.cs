namespace HumanResourcesWebApi.Models.DTO;

public class TabelBulletinDTO
{
    public int Id { get; set; }
    public string? Serial { get; set; }
    public string? Number { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? InvalidityBeginDate { get; set; }
    public DateTime? InvalidityEndDate { get; set; }
    public bool InvalidityContinues { get; set; }
    public string? Note { get; set; }
}
