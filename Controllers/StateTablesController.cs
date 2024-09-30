using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Common.Mapper;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateTablesController : ControllerBase
{
    private readonly IStateTablesRepository _repos;

    public StateTablesController(IStateTablesRepository repos) => _repos = repos;

    [HttpGet]
    public async Task<IActionResult> GetStateTables(int itemsPerPage = 30, int currentPage = 1, bool showOnlyActive = true)
    {
        try
        {
            var (stateTables, pageInfo) = await _repos.GetStateTablesAsync(itemsPerPage, currentPage, showOnlyActive);
            var result = new List<StateTableInfoDTO>();
            foreach (var stateTable in stateTables)
            {
                result.Add(StateTable_StateTableDtoMapper.MapDto(stateTable));
            }
            return Ok(new { StateTables = result, PageInfo = pageInfo });
        }

        catch (SqlException ex)
        {
            return Conflict($"SQL Exception:\n Error Code: {ex.ErrorCode}\nError Message: {ex.Message}");
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }

    }

    [HttpGet("{organizationId}")]
    public async Task<IActionResult> GetStateTablesByOrganization(int organizationId, [FromQuery] bool showOnlyActive = true)
    {
        try
        {
            var stateTables = await _repos.GetByOrganizationAsync(organizationId, showOnlyActive);
            var result = new List<StateTableInfoDTO>();
            foreach (var stateTable in stateTables)
            {
                result.Add(StateTable_StateTableDtoMapper.MapDto(stateTable));
            }
            return Ok(result);

        }
        catch (SqlException ex)
        {
            return Conflict($"SQL Exception:\n Error Code: {ex.ErrorCode}\nError Message: {ex.Message}");
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddStateTable([FromForm] AddStateTableRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _repos.AddStateTableAsync(request);
            return StatusCode(StatusCodes.Status201Created, "State table added successfully.");
        }
        catch (SqlException ex)
        {
            return Conflict($"SQL Exception: Error Code: {ex.ErrorCode} Message: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStateTable([FromForm] UpdateStateTableRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _repos.UpdateStateTableAsync(request);

            return Ok(new { Message = "State Table updated successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { Message = $"SQL Exception: {ex.Message}" });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = $"Not Found: {ex.Message}" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An error occurred: {ex.Message}" });
        }
    }

}
