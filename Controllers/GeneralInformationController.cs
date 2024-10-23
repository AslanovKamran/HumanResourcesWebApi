using HumanResourcesWebApi.Models.Requests.GeneralInformation;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneralInformationController(IGeneralInformationRepository repos) : ControllerBase
{
    private readonly IGeneralInformationRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves the list of general information records.
    /// </summary>
    /// <returns>
    /// A list of general information records if available, otherwise a 404 Not Found response.
    /// </returns>

    [HttpGet]
    public async Task<IActionResult> GetGeneralInformation()
    {
        try
        {
            var result = await _repos.GetGeneralInformationAsync();
            if (result == null || result.Count == 0)
                return NotFound($"No genral information found.");

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

    #region Update
    
    /// <summary>
    /// Updates an existing general information record.
    /// </summary>
    /// <param name="request">The request object containing updated general information.</param>
    /// <returns>
    /// A success message if the update is successful, or a validation error or server error response.
    /// </returns>
    
    [HttpPut]
    public async Task<IActionResult> UpdateGeneralInformation([FromForm] UpdateGeneralInformationRequest request)
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
            await _repos.UpdateGeneralInformationAsync(request);
            return Ok(new { message = "General Information updated successfully." });
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
