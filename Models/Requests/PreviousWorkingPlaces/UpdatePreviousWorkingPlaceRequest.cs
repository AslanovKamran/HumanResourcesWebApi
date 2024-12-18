using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.PreviousWorkingPlaces
{
    public class UpdatePreviousWorkingPlaceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string OrganizationName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings =false)]
        public string Position { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
