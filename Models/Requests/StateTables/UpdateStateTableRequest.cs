using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.StateTables
{
    public class UpdateStateTableRequest
    {
        [Required]
        public int Id { get; set; } // Primary Key, required for update

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Degree must be non-negative")]
        public int? Degree { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Unit Count must be non-negative")]
        public int? UnitCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Monthly Salary From must be non-negative")]
        public int? MonthlySalaryFrom { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Hourly Salary must be non-negative")]
        public decimal? HourlySalary { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Monthly Salary Extra must be non-negative")]
        public int? MonthlySalaryExtra { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Occupied Post Count must be non-negative")]
        public int? OccupiedPostCount { get; set; }

        [MaxLength(100)]
        public string? DocumentNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessage = "WorkTypeId is required")]
        public int WorkTypeId { get; set; } // Foreign key to StateWorkTypes

        [Required(ErrorMessage = "OrganizationStructureId is required")]
        public int OrganizationStructureId { get; set; } // Foreign key to OrganizationStructures

        [Range(0, int.MaxValue, ErrorMessage = "Harmfulness Coefficient must be non-negative")]
        public int? HarmfulnessCoefficient { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Work Hours must be non-negative")]
        public int? WorkHours { get; set; }

        [MaxLength(100)]
        public string? WorkingHoursSpecial { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Work Hours Saturday must be non-negative")]
        public int? WorkHoursSaturday { get; set; }

        public int? TabelPriority { get; set; }

        public int? TabelPosition { get; set; }

        public bool IsCanceled { get; set; }
    }

}
