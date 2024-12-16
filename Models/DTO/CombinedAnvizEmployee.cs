namespace HumanResourcesWebApi.Models.DTO;

public class CombinedAnvizEmployee
{
    public string Intime { get; set; } = string.Empty; // From Anviz
    public string OutTime { get; set; } = string.Empty; // From Anviz

    public string TabelNumber { get; set; } = string.Empty; // From AnvizEmployee
    public string Name { get; set; } = string.Empty; // From AnvizEmployee
    public string Surname { get; set; } = string.Empty; // From AnvizEmployee
    public string FatherName { get; set; } = string.Empty;// From AnvizEmployee
    public string Organization { get; set; } = string.Empty; // From AnvizEmployee
    public string Position { get; set; } = string.Empty; // From AnvizEmployee
    public int Degree { get; set; } // From AnvizEmployee
    public string WorkType { get; set; } = string.Empty; // From AnvizEmployee
    public string AnvisUserId { get; set; } = string.Empty; // From AnvizEmployee
    public int WorkHours { get; set; } // From AnvizEmployee
}
