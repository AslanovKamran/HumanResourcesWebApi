using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnvizController : ControllerBase
{
    private readonly IAnvizRepository _anvizRepos;
    private readonly IAnvizEmployeesRepository _anvizEmployeeRepos;

    public AnvizController(IAnvizRepository repos, IAnvizEmployeesRepository anvizEmployeeRepos)
    {
        _anvizRepos = repos;
        _anvizEmployeeRepos = anvizEmployeeRepos;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnvizByDateRange(DateTime startDate, DateTime endDate, int? organizationId)
    {
        if (startDate > endDate)
        {
            return BadRequest(new { message = "StartDate cannot be greater than EndDate." });
        }

        try
        {
            var anvizData = await _anvizRepos.GetCheckDateAsync(startDate, endDate);
            var employeeData = await _anvizEmployeeRepos.GetAnvizEmployeesAsync(organizationId);

            var combinedData = GetCombinedData(anvizData, employeeData);
            return Ok(combinedData);

        }
        catch (SqlException ex)
        {
            return StatusCode(409, new { message = "SQL Error: ", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred: ", details = ex.Message });
        }
    }

    private List<CombinedAnvizEmployee> GetCombinedData(List<Anviz> anvizData, List<AnvizEmployee> employeeData)
    {

        // Join the data using LINQ
        var combinedData = employeeData
       .GroupJoin(
           anvizData,
           employee => employee.AnvisUserId,   // Match AnvisUserId from employeeData
           anviz => anviz.UserId,             // Match UserId from anvizData
           (employee, anvizMatches) => new { employee, anvizMatches })
       .SelectMany(
           x => x.anvizMatches.DefaultIfEmpty(), // Use DefaultIfEmpty to include employees with no matches
           (x, anviz) => new CombinedAnvizEmployee
           {
               Intime = anviz?.Intime ?? "",   // Default to empty string if no match
               OutTime = anviz?.OutTime ?? "", // Default to empty string if no match
               TabelNumber = x.employee.TabelNumber,
               Name = x.employee.Name,
               Surname = x.employee.Surname,
               FatherName = x.employee.FatherName,
               Organization = x.employee.Organization,
               Position = x.employee.Position,
               Degree = x.employee.Degree,
               WorkType = x.employee.WorkType,
               AnvisUserId = x.employee.AnvisUserId,
               WorkHours = x.employee.WorkHours
           })
       .OrderBy(x => x.Organization) // Order by Organization in ascending order
       .ToList();
        return combinedData;
    }
}
