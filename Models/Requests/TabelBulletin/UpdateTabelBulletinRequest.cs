namespace HumanResourcesWebApi.Models.Requests.TabelBulletin;

public class UpdateTabelBulletinRequest
{
    public int Id { get; set; }
    public string? Serial { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public DateTime InvalidityBeginDate { get; set; }
    public DateTime InvalidityEndDate { get; set; }
    public string? Note { get; set; }
    public bool InvalidityContinues { get; set; }
    public string? UpdatedUser { get; set; }
}
