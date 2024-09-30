using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateTablesController : ControllerBase
{
    private readonly IStateTablesRepository _repos;

    public StateTablesController(IStateTablesRepository repos) => _repos = repos;

    [HttpGet]
    public async Task<IActionResult> GetStateTables(int itemsPerPage = 10, int currentPage = 1, bool showOnlyActive = true)
    {
        var (stateTables, pageInfo) = await _repos.GetStateTablesAsync(itemsPerPage, currentPage, showOnlyActive);
        var result = new List<StateTableInfoDTO>();
        foreach (var stateTable in stateTables) 
        {
            result.Add(MapDto(stateTable));
        }
        return Ok(new { StateTables = result, PageInfo = pageInfo });
    }


    private static StateTableInfoDTO MapDto(StateTable obj) 
    {
        var result = new StateTableInfoDTO();

        result.Id = obj.Id;
        result.OrganizatinStructureFullName = obj.OrganizationStructure.FullName;
        result.Name = obj.Name;
        result.Degree = obj.Degree;
        result.UnitCount = obj.UnitCount;
        result.MonthlySalaryFrom = obj.MonthlySalaryFrom;
        result.HourlySalary = obj.HourlySalary;
        result.MonthlySalaryExtra = obj.MonthlySalaryExtra;
        result.OccupiedPostCount = obj.OccupiedPostCount;
        result.DocumentNumber = obj.DocumentNumber;
        result.DocumentDate = obj.DocumentDate;
        result.StateWorkType = obj.StateWorkType.Type;
        result.HarmfulnessCoefficient = obj.HarmfulnessCoefficient;
        result.WorkHours = obj.WorkHours;
        result.WorkHoursSaturday = obj.WorkHoursSaturday;
        result.TabelPosition = obj.TabelPosition;
        result.TabelPriority = obj.TabelPriority;
        result.IsCanceled = obj.IsCanceled;

        return result;
    }
}
