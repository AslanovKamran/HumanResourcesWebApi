namespace HumanResourcesWebApi.Models.DTO
{
    public class EmployeesChunk
    {
        public string? PhotoUrl { get; set; }  // Nullable as it's not required
        public string Surname { get; set; } = string.Empty;   // Not nullable, assuming it's required
        public string Name { get; set; } = string.Empty;     // Not nullable, assuming it's required
        public string FatherName { get; set; } = string.Empty;// Not nullable, assuming it's required
        public DateTime? BirthDate { get; set; } // Nullable as BirthDate can be null
        public string GenderType { get; set; } = string.Empty;// Not nullable
        public string MaritalStatus { get; set; } = string.Empty; // Not nullable
        public string? SocialInsuranceNumber { get; set; } // Nullable
        public string? TabelNumber { get; set; } // Nullable
        public DateTime EntryDate { get; set; }  // Not nullable, assuming it's required
        public int? TrainershipYear { get; set; } // Nullable
        public int? TrainershipMonth { get; set; } // Nullable
        public int? TrainershipDay { get; set; } // Nullable
        public string OrganizationStructureName { get; set; } = string.Empty;// Not nullable
        public string StateTableName { get; set; } = string.Empty;// Not nullable
        public string StateTableDegree { get; set; } = string.Empty; // Not nullable
        public bool IsWorking { get; set; } // Not nullable, since it's a BIT (boolean)
        public string TotalExperience
        {
            get
            {
                // Calculate the difference between EntryDate and the current date
                var currentDate = DateTime.Now;
                var experienceSpan = currentDate - EntryDate;

                // Add Trainership Year, Month, Day if available
                int totalYears = experienceSpan.Days / 365;
                int remainingDays = experienceSpan.Days % 365;
                int totalMonths = remainingDays / 30;
                int totalDays = remainingDays % 30;

                if (TrainershipYear.HasValue)
                    totalYears += TrainershipYear.Value;

                if (TrainershipMonth.HasValue)
                    totalMonths += TrainershipMonth.Value;

                if (TrainershipDay.HasValue)
                    totalDays += TrainershipDay.Value;

                // Normalize months and days if needed (e.g., 12 months -> 1 year)
                if (totalMonths >= 12)
                {
                    totalYears += totalMonths / 12;
                    totalMonths = totalMonths % 12;
                }

                return $"{totalYears} years, {totalMonths} months, {totalDays} days";
            }
        }
    }

}
