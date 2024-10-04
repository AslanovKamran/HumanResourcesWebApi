namespace HumanResourcesWebApi.Common.Filters
{
    public class EmployeeFilter
    {
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public DateTime? BirthDateStart { get; set; }
        public DateTime? BirthDateEnd { get; set; }
        public DateTime? EntryDateStart { get; set; }
        public DateTime? EntryDateEnd { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public bool? HasPoliticalParty { get; set; }
        public bool? HasSocialInsuranceNumber { get; set; }
        public string? TabelNumber { get; set; }
        public string? AnvisUserId { get; set; }
        public bool? IsWorking { get; set; }
        public string? OrganizationFullName { get; set; }
    }
}
