using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.BrigadeReplacements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrigadeReplacementsController : ControllerBase
{
    private readonly IBrigadeReplacementsRepository _repos;
    public BrigadeReplacementsController(IBrigadeReplacementsRepository repos) => _repos = repos;

    /// <summary>
    /// Get the list of brigade replacements.
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _repos.GetBrigadeReplacementsAsync();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No replacements found" });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }
    /// <summary>
    /// Add a new Brigade Replacement.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddBrigadeReplacementRequest request)
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
            await _repos.AddBrigadeReplacementAsync(request);
            return Ok(new { message = "Brigade replacement added successfully." });
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

    /// <summary>
    /// Delete an existing brigade replacement.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteBrigadeReplacementAsync(id);
            return Ok(new { message = "Brigade replacement deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the replacement.", details = ex.Message });
        }
    }
}
