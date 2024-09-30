namespace HumanResourcesWebApi.Models.DTO
{
    public class StateTableInfoDTO
    {
        public int Id { get; set; }
        public string OrganizatinStructureFullName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? Degree { get; set; }
        public int UnitCount { get; set; }
        public int? MonthlySalaryFrom { get; set; }
        public decimal? HourlySalary { get; set; }
        public int? MonthlySalaryExtra { get; set; }
        public int? OccupiedPostCount { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string StateWorkType { get; set; } = string.Empty;
        public int? HarmfulnessCoefficient { get; set; }
        public int? WorkHours { get; set; }
        public int? WorkHoursSaturday { get; set; }
        public int? TabelPosition { get; set; }
        public int? TabelPriority { get; set; }
        public bool IsCanceled { get; set; }
    }
}
