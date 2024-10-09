using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.Employees;

public class UpdateEmployeeGeneralInfoRequest
{
    [Required]
    public int Id { get; set; }

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string Surname { get; set; } = string.Empty;

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string FatherName { get; set; } = string.Empty;

    [Url]
    [StringLength(255)]
    public string? PhotoUrl { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [StringLength(255)]
    public string BirthPlace { get; set; } = string.Empty;

    public int NationalityId { get; set; }
    public int GenderId { get; set; }
    public int MaritalStatusId { get; set; }

    [StringLength(255)]
    public string? SocialInsuranceNumber { get; set; }

    [StringLength(255)]
    public string? TabelNumber { get; set; }

    [StringLength(255)]
    public string? AnvisUserId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Year must be a positive number")]
    public int? TrainershipYear { get; set; }

    [Range(0, 11, ErrorMessage = "Month must be between 0 and 11")]
    public int? TrainershipMonth { get; set; }

    [Range(0, 30, ErrorMessage = "Days must be between 0 and 30")]
    public int? TrainershipDay { get; set; }

    [StringLength(255)]
    public string? RegistrationAddress { get; set; }

    [StringLength(255)]
    public string? LivingAddress { get; set; }

    [Phone]
    [StringLength(255)]
    public string? MobileNumber { get; set; }

    [Phone]
    [StringLength(255)]
    public string? MobileNumber2 { get; set; }

    [Phone]
    [StringLength(255)]
    public string? MobileNumber3 { get; set; }

    [Phone]
    [StringLength(255)]
    public string? TelephoneNumber { get; set; }

    [StringLength(100)]
    public string? InternalNumber { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }

    public bool IsTradeUnionMember { get; set; }
    public bool IsVeteran { get; set; }
    public bool HasWarInjury { get; set; }

    [Range(1, 3, ErrorMessage = "Disability degree must be between 1 and 3")]
    public int? DisabilityDegree { get; set; }
    public bool HasDisabledChild { get; set; }
    public bool IsRefugeeFromAnotherCountry { get; set; }
    public bool IsRefugee { get; set; }
}
