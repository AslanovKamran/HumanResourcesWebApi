﻿using System.ComponentModel.DataAnnotations;

namespace HumanResourcesWebApi.Models.Requests.BusinessTrips;

public class UpdateBusinessTripRequest
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(255)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }

    public string? DocumentNumber { get; set; }

    public DateTime? DocumentDate { get; set; }

    [Required]
    public DateTime TripCardGivenAt { get; set; }

    [StringLength(50)]
    public string? TripCardNumber { get; set; }

    [StringLength(100)]
    public string? OrganizationInCharge { get; set; }

    [StringLength(300)]
    public string? Note { get; set; }
}
