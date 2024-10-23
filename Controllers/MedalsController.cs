using HumanResourcesWebApi.Models.Requests.Medals;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedalsController(IMedalsRepository repos) : ControllerBase
{
    private readonly IMedalsRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a list of medals for a specific employee by their ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose medals are being retrieved.</param>
    /// <returns>A list of medals associated with the specified employee, or a 404 if none are found.</returns>
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetMedals(int employeeId)
    {
        try
        {
            var medals = await _repos.GetMedalsAsync(employeeId);
            if (medals == null || medals.Count == 0)
            {
                return NotFound(new { message = "No medals found for the specified employee." });
            }
            return Ok(medals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new medal for an employee.
    /// </summary>
    /// <param name="request">The details of the medal to be added.</param>
    /// <returns>A success message if the medal is added, or an error if validation fails or a server error occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> AddMedal([FromForm] AddMedalRequest request)
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
            await _repos.AddMedalAsync(request);
            return Ok(new { message = "Medal added successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the medal.", details = ex.Message });
        }
    }

    #endregion

    #region Update
    
    /// <summary>
    /// Updates an existing medal for an employee.
    /// </summary>
    /// <param name="request">The updated medal details.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a server error occurs.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateMedal([FromForm] UpdateMedalRequest request)
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
            await _repos.UpdateMedalAsync(request);
            return Ok(new { message = "Medal updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the medal.", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes a medal by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the medal to delete.</param>
    /// <returns>A success message if the deletion is successful, or an error if a server error occurs.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedal(int id)
    {
        try
        {
            await _repos.DeleteMedalAsync(id);
            return Ok(new { message = "Medal deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the medal.", details = ex.Message });
        }
    }

    #endregion

}
