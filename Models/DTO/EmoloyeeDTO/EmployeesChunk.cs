using HumanResourcesWebApi.Common.Calculations;

namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO
{
    public class EmployeesChunk
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }  // Nullable as it's not required
        public string Surname { get; set; } = string.Empty;   // Not nullable, assuming it's required
        public string Name { get; set; } = string.Empty;     // Not nullable, assuming it's required
        public string FatherName { get; set; } = string.Empty;// Not nullable, assuming it's required
        public DateTime? BirthDate { get; set; } // Nullable as BirthDate can be null

        public int? Age
        {
            get
            {
                if (BirthDate == null)
                    return null; // Return null if BirthDate is not provided

                var today = DateTime.Today;
                var age = today.Year - BirthDate.Value.Year;

                // Adjust for cases where the birthday hasn't occurred this year
                if (BirthDate.Value.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }

        public string GenderType { get; set; } = string.Empty;// Not nullable
        public string MaritalStatus { get; set; } = string.Empty; // Not nullable
        public string? SocialInsuranceNumber { get; set; } // Nullable
        public string? TabelNumber { get; set; } // Nullable
        public DateTime? EntryDate { get; set; }  // Not nullable, assuming it's required

        public DateTime? QuitDate { get; set; }  // Not nullable, assuming it's required
        public int? TrainershipYear { get; set; } // Nullable
        public int? TrainershipMonth { get; set; } // Nullable
        public int? TrainershipDay { get; set; } // Nullable
        public string OrganizationStructureName { get; set; } = string.Empty;// Not nullable
        public string StateTableName { get; set; } = string.Empty;// Not nullable
        public string StateTableDegree { get; set; } = string.Empty; // Not nullable
        public bool IsWorking { get; set; } // Not nullable, since it's a BIT (boolean)

        public string TotalExperience =>
             $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).years} years " +
             $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).months} months " +
             $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).days} days";

    }

}
