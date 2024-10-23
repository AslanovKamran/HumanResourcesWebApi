using HumanResourcesWebApi.Models.Requests.OrganizationStructures;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationStructuresController(IOrganizationStructuresRepository repos) : ControllerBase
{
    private readonly IOrganizationStructuresRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a list of organization structures, with an option to include canceled organizations.
    /// </summary>
    /// <param name="includeCanceled">Indicates whether canceled organization structures should be included.</param>
    /// <returns>A list of organization structures based on the specified filter.</returns>
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations([FromQuery] bool includeCanceled = false)
    {
        var organizationStructures = await _repos.GetOrganizationStructureListAsync(includeCanceled);
        return Ok(organizationStructures);
    }

    /// <summary>
    /// Retrieves an organization structure by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the organization structure to retrieve.</param>
    /// <returns>The organization structure with the specified ID, or a 404 if not found.</returns>
  
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        var organizationStructure = await _repos.GetOrganizationStructureByIdAsync(id);
        return Ok(organizationStructure);
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new organization structure.
    /// </summary>
    /// <param name="request">The details of the organization structure to be added.</param>
    /// <returns>The added organization structure, or an error if validation fails or a server error occurs.</returns>

    [HttpPost]
    public async Task<IActionResult> AddOrganizationStructure([FromForm] AddOrganizationStructureRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var addedOrganization = await _repos.AddOrganizationStructureAsync(request);
            return Ok(addedOrganization);
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

    #region Update

    /// <summary>
    /// Updates an existing organization structure.
    /// </summary>
    /// <param name="request">The updated organization structure details.</param>
    /// <returns>The updated organization structure, or an error if validation fails or a server error occurs.</returns>
  
    [HttpPut]
    public async Task<IActionResult> UpdateOrganizationStructure([FromForm] UpdateOrganizationStructureRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _repos.UpdateOrganizationStrcuture(request);
            return Ok(updated);
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

    #region Delete
    
    /// <summary>
    /// Soft deletes an organization structure by marking it as canceled.
    /// </summary>
    /// <param name="id">The ID of the organization structure to cancel.</param>
    /// <returns>A success message if the organization structure is canceled, or an error if a server error occurs.</returns>
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteOrganizationStructure(int id)
    {
        try
        {
            await _repos.DeleteOrganizationStructureAsync(id);
            return Ok(new { Message = "Organization structure successfully marked as canceled." });
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
