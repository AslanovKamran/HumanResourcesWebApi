﻿namespace HumanResourcesWebApi.Models.DTO;

public class WorkNormDTO
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string Month { get; set; } = string.Empty;
    public int MonthlyWorkingHours { get; set; }
}