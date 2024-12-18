using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.PreviousNames;
using HumanResourcesWebApi.Models.Requests.PreviousWorkingPlaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PreviousWorkingPlacesController : ControllerBase
{
    private readonly IPreviousWorkingPlacesRepository _repos;
    public PreviousWorkingPlacesController(IPreviousWorkingPlacesRepository repos) => _repos = repos;

    #region Get

    /// <summary>
    ///  Retrieves a list of previous working places for a given employee.
    /// </summary>
    /// <param name="employeeId"></param>

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeePrevWorkingPlaces(int employeeId) 
    {
        try
        {
            var result = await _repos.GetPreviousWorkingPlacesByEmployeeIdAsync(employeeId);
            if (result is null || result.Count == 0)
                return NotFound(new { message = "No previous working places found for this employee." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new previous working place for an employee.
    /// </summary>
    /// <param name="request">The request containing the previous working place details.</param>
    /// <returns>Returns success message on successful creation.</returns>

    [HttpPost]
    public async Task<IActionResult> AddPreviousWorkPlace([FromForm] AddPreviousWorkingPlaceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        try
        {
            await _repos.AddPreviousWorkingPlaceAsync(request);
            return Ok(new { message = "Previous work place added successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Update

    /// <summary>
    /// Updates an existing previous work place for an employee.
    /// </summary>
    /// <param name="request">The request containing the updated previous work place details.</param>
    /// <returns>Returns success message on successful update.</returns>

    [HttpPut]
    public async Task<IActionResult> UpdatePreviousWorkPlace([FromForm] UpdatePreviousWorkingPlaceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        try
        {
            await _repos.UpdatePreviousWorkingPlaceAsync(request);
            return Ok(new { message = "Previous work place updated successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes an existing previous work place for an employee.
    /// </summary>
    /// <param name="id">The ID of the previous work place to delete.</param>
    /// <returns>Returns success message on successful deletion.</returns>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePreviousWprkPlace(int id)
    {
        try
        {
            await _repos.DeletePreviousWorkingPlaceAsync(id);
            return Ok(new { message = "Previous work place deleted successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion
}
