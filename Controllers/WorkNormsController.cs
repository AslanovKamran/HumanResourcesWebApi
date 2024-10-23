using HumanResourcesWebApi.Models.Requests.WorkNorms;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkNormsController(IWorkNormsRepository repos) : ControllerBase
{
    private readonly IWorkNormsRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves work norms for a specific year. If no year is provided, it retrieves norms for the current year.
    /// </summary>
    /// <param name="year">The year for which to retrieve work norms. If not provided, the current year will be used.</param>
    /// <returns>
    /// A list of work norms for the specified year, or an error message if no work norms are found.
    /// </returns>
    
    [HttpGet("{year?}")]
    public async Task<IActionResult> GetWorkNorms(int? year)
    {
        try
        {
            var result = await _repos.GetWorkNormsAsync(year);
            if (result == null || result.Count == 0)
            {
                return NotFound($"No work norms found for the {year ?? DateTime.Now.Year} year");
            }
            return Ok(result);
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

    #region Add

    /// <summary>
    /// Adds a new work norm.
    /// </summary>
    /// <param name="request">The request object containing details of the work norm to add.</param>
    /// <returns>
    /// A message indicating whether the work norm was added successfully or an error response.
    /// </returns>

    [HttpPost]
    public async Task<IActionResult> AddWorkNorm([FromForm] AddWorkNormRequest request)
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
            await _repos.AddWorkNormAsync(request);
            return Ok(new { message = "Work norm added successfully." });
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
    /// Updates an existing work norm.
    /// </summary>
    /// <param name="request">The request object containing the updated details of the work norm.</param>
    /// <returns>
    /// A message indicating whether the work norm was updated successfully or an error response.
    /// </returns>

    [HttpPut]
    public async Task<IActionResult> UpdateWorkNorm([FromForm] UpdateWorkNormRequest request)
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
            await _repos.UpdateWorkNormAsync(request);
            return Ok(new { message = "Work norm updated successfully." });
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
    /// Deletes a work norm by its ID.
    /// </summary>
    /// <param name="id">The ID of the work norm to delete.</param>
    /// <returns>
    /// A message indicating whether the work norm was deleted successfully or an error response.
    /// </returns>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkNorm(int id)
    {

        try
        {
            await _repos.DeleteWorkNormAsync(id);
            return Ok(new { message = "Work norm deleted successfully." });
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
