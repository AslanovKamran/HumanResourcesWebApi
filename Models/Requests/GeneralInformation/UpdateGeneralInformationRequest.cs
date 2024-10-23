using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.GeneralInformation;

public class UpdateGeneralInformationRequest
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Text { get; set; }
}
