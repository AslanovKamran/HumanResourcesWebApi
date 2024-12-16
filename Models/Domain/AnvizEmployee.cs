namespace HumanResourcesWebApi.Models.Domain;

public class AnvizEmployee
{
    public string TabelNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int Degree { get; set; }
    public string WorkType { get; set; } = string.Empty;
    public string AnvisUserId { get; set; } = string.Empty;
    public int WorkHours{ get; set; }
}
