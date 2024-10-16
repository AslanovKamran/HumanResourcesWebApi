using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.PoliticalParties;

public class UpdatePoliticalPartyRequest
{
    [Required]
    public int Id { get; set; }

    [StringLength(255, ErrorMessage = "Political Party cannot exceed 255 characters.")]
    public string? PoliticalParty { get; set; }

    [StringLength(255, ErrorMessage = "Party Membership Number cannot exceed 255 characters.")]
    public string? PartyMembershipNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PartyEntranceDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PartyCardGivenDate { get; set; }

    [StringLength(255, ErrorMessage = "Party Organization Region cannot exceed 255 characters.")]
    public string? PartyOrganizationRegion { get; set; }
}
