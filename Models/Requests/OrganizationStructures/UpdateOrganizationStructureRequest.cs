using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.OrganizationStructures
{
    public class UpdateOrganizationStructureRequest
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public required string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BeginningHistory { get; set; }

        public string? FirstNumber { get; set; }

        public string? SecondNumber { get; set; }

        public int? TabelOrganizationId { get; set; }

        public int? TabelPriority { get; set; }

        public bool Canceled { get; set; }

        [MaxLength(200)]
        public string? HeadName { get; set; }

        [MaxLength(200)]
        public string? HeadPosition { get; set; }

        public bool IsSeaCoef { get; set; }
    }
}
