namespace HumanResourcesWebApi.Models.DTO
{
    public class EmployeeMedal
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string MedalType { get; set; } = string.Empty;
        public string? OrderNumber { get; set; }
        public string? Note { get; set; }
        public int EmployeeId { get; set; }
    }
}
