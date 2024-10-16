﻿using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Medals;

namespace HumanResourcesWebApi.Abstract;

public interface IMedalsRepository
{
    public Task<List<EmployeeMedal>> GetMedalsAsync(int employeeId);
    public Task AddMedalAsync(AddMedalRequest request);
    public Task UpdateMedalAsync(UpdateMedalRequest request);
    public Task DeleteMedalAsync(int id);
}