namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO
{
    public class EmployeeEducation
    {
        public int Id { get; set; }

        public string EducationType { get; set; } = string.Empty;

        public string? Institution { get; set; } = string.Empty;

        public string? Speciality { get; set; } = string.Empty;

        public string? EducationKind { get; set; } = string.Empty;

        public DateTime? EducationStartedAt { get; set; }

        public DateTime? EducationEndedAt { get; set; }
        public string? DiplomaType { get; set; } = string.Empty;
        public string? DiplomaNumber { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
    }
}
