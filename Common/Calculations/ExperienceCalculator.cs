namespace HumanResourcesWebApi.Common.Calculations
{
    public static class ExperienceCalculator
    {
        public static (int years, int months, int days) CalculatePreviousExperience(int? trainershipYear, int? trainershipMonth, int? trainershipDay)
        {
            int years = trainershipYear ?? 0;
            int months = trainershipMonth ?? 0;
            int days = trainershipDay ?? 0;

            return (years, months, days);
        }

        public static (int years, int months, int days) CalculateCurrentExperience(DateTime? entryDate, DateTime? quitDate)
        {
            if (entryDate is null) return (0, 0, 0);
            var startDate = entryDate.Value;
            var endDate = quitDate ?? DateTime.Now;

            TimeSpan range = endDate - startDate;

            // Convert the total number of days to years, months, and days
            int years = (int)(range.TotalDays / 365.25); // Accounting for leap years
            int remainingDays = (int)(range.TotalDays % 365.25);

            int months = remainingDays / 30;  // Approximate month length
            int days = remainingDays % 30;

            return (years, months, days);
        }

        public static (int years, int months, int days) CalculateTotalExperience(int? trainershipYear, int? trainershipMonth, int? trainershipDay, DateTime? entryDate, DateTime? quitDate)
        {
            // Calculate previous and current experience
            var (prevYears, prevMonths, prevDays) = CalculatePreviousExperience(trainershipYear, trainershipMonth, trainershipDay);
            var (currentYears, currentMonths, currentDays) = CalculateCurrentExperience(entryDate, quitDate);

            // Add years, months, and days
            int totalYears = prevYears + currentYears;
            int totalMonths = prevMonths + currentMonths;
            int totalDays = prevDays + currentDays;

            // Adjust if days exceed 30 (general rule for days in a month)
            if (totalDays >= 30)
            {
                totalMonths += totalDays / 30;
                totalDays %= 30;
            }

            // Adjust if months exceed 12
            if (totalMonths >= 12)
            {
                totalYears += totalMonths / 12;
                totalMonths %= 12;
            }
            return (totalYears, totalMonths, totalDays);
        }
    }
}
