using HumanResourcesWebApi.Models.Requests.StateTables;
using HumanResourcesWebApi.Common.Mapper;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateTablesController(IStateTablesRepository repos) : ControllerBase
{
    private readonly IStateTablesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a paginated list of state tables.
    /// </summary>
    /// <param name="itemsPerPage">The number of state tables per page.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="showOnlyActive">Specifies whether to show only active state tables.</param>
    /// <returns>
    /// A list of state tables with pagination information.
    /// </returns>
   
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
            return Ok(new
            {
                StateTables = result,
                PageInfo = pageInfo
            });
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

    /// <summary>
    /// Retrieves a list of state tables for a specific organization.
    /// </summary>
    /// <param name="organizationId">The ID of the organization.</param>
    /// <param name="showOnlyActive">Specifies whether to show only active state tables for the organization.</param>
    /// <returns>
    /// A list of state tables for the specified organization.
    /// </returns>
    
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

    #endregion

    #region Add
    
    /// <summary>
    /// Adds a new state table.
    /// </summary>
    /// <param name="request">The request object containing the details of the state table to add.</param>
    /// <returns>
    /// A message indicating the success of the operation or an error response.
    /// </returns>

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

    #endregion

    #region Update

    /// <summary>
    /// Updates an existing state table.
    /// </summary>
    /// <param name="request">The request object containing the updated state table details.</param>
    /// <returns>
    /// A message indicating the success of the update operation or an error response.
    /// </returns>
    /// 
    
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

    #endregion

    #region Delete

    /// <summary>
    /// Deletes (marks as canceled) a state table by its ID.
    /// </summary>
    /// <param name="id">The ID of the state table to delete.</param>
    /// <returns>
    /// A message indicating the success of the deletion or an error response.
    /// </returns>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStateTable(int id)
    {
        try
        {
            await _repos.DeleteStateTableAsync(id);
            return Ok(new { Message = "State table successfully marked as canceled." });
        }

        catch (SqlException ex)
        {
            return Conflict(new { Message = $"SQL Exception: {ex.Message}" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An error occurred: {ex.Message}" });
        }
    }

    #endregion

}
