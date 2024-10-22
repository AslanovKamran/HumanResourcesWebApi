using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.VacationTypes;

public class AddVacationTypeRequest
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings =false)]
    [StringLength(255, ErrorMessage = "Vacation Name cannot exceed 255 characters.")]
    public string Name{ get; set; } = string.Empty;

    [Required]
    public int VacationPaymentTypeId { get; set; }
}
