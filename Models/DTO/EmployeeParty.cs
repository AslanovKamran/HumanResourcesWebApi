namespace HumanResourcesWebApi.Models.DTO
{
    public class EmployeeParty
    {
        public int Id { get; set; }
        public string? PoliticalParty { get; set; }
        public string? PartyMembershipNumber { get; set; }
        public DateTime? PartyEntranceDate { get; set; }
        public DateTime? PartyCardGivenDate { get; set; }
        public string? PartyOrganizationRegion { get; set; }
    }
}
