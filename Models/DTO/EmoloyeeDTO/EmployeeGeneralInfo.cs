using HumanResourcesWebApi.Common.Calculations;

namespace HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;

public class EmployeeGeneralInfoDto
{
    public int Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;

    public string? SocialInsuranceNumber { get; set; }
    public string? TabelNumber { get; set; }
    public string? AnvisUserId { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? QuitDate { get; set; }

    // Trainership period (nullable because each component can be null)
    public int? TrainershipYear { get; set; }
    public int? TrainershipMonth { get; set; }
    public int? TrainershipDay { get; set; }

    public string? RegistrationAddress { get; set; }
    public string? LivingAddress { get; set; }

    // Multiple phone numbers
    public string? MobileNumber { get; set; }
    public string? MobileNumber2 { get; set; }
    public string? MobileNumber3 { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? InternalNumber { get; set; }
    public string? Email { get; set; }

    // Organizational structure
    public string OrganizationStructureName { get; set; } = string.Empty;
    public string StateTableName { get; set; } = string.Empty;
    public int? StateTableDegree { get; set; }

    // Employee status flags
    public bool IsTradeUnionMember { get; set; }
    public bool IsVeteran { get; set; }
    public bool HasWarInjury { get; set; }
    public int? DisabilityDegree { get; set; }
    public bool HasDisabledChild { get; set; }
    public bool IsRefugee { get; set; }
    public bool IsRefugeeFromAnotherCountry { get; set; }

    public string PreviousExperience =>
        $"{ExperienceCalculator.CalculatePreviousExperience(TrainershipYear, TrainershipMonth, TrainershipDay).years} years " +
        $"{ExperienceCalculator.CalculatePreviousExperience(TrainershipYear, TrainershipMonth, TrainershipDay).months} months " +
        $"{ExperienceCalculator.CalculatePreviousExperience(TrainershipYear, TrainershipMonth, TrainershipDay).days} days";
    public string CurrentExperience =>
        $"{ExperienceCalculator.CalculateCurrentExperience(EntryDate, QuitDate).years} years " +
        $"{ExperienceCalculator.CalculateCurrentExperience(EntryDate, QuitDate).months} months " +
        $"{ExperienceCalculator.CalculateCurrentExperience(EntryDate, QuitDate).days} days";


    public string TotalExperience =>
      $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).years} years " +
      $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).months} months " +
      $"{ExperienceCalculator.CalculateTotalExperience(TrainershipYear, TrainershipMonth, TrainershipDay, EntryDate, QuitDate).days} days";



    // Method to calculate PreviousExperience based on Trainership data





}

