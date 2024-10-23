using HumanResourcesWebApi.Models.Requests.Vacations;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationsController(IVacationsRepository repos) : ControllerBase
{
    private readonly IVacationsRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves vacations for a specific employee, with optional filters for start and end years.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="yearStarted">The optional start year filter for vacations.</param>
    /// <param name="yearEnded">The optional end year filter for vacations.</param>
    /// <returns>
    /// A list of vacations for the specified employee within the provided year range, if available.
    /// </returns>
   
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetVacations(int employeeId, int? yearStarted = null, int? yearEnded = null)
    {
        try
        {
            var vacations = await _repos.GetVacationsAsync(employeeId, yearStarted, yearEnded);
            if (vacations == null || vacations.Count == 0)
            {
                return NotFound($"No vacations found for employee ID: {employeeId}");
            }
            return Ok(vacations);
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
    /// Adds a new vacation for an employee.
    /// </summary>
    /// <param name="request">The request object containing the details of the vacation to add.</param>
    /// <returns>
    /// A message indicating whether the vacation was added successfully or an error response.
    /// </returns>
    
    [HttpPost]
    public async Task<IActionResult> AddVacation([FromForm] AddVacationRequest request)
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
            await _repos.AddVacationAsync(request);
            return Ok(new { message = "Vacation added successfully." });
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
    /// Updates an existing vacation for an employee.
    /// </summary>
    /// <param name="request">The request object containing the updated vacation details.</param>
    /// <returns>
    /// A message indicating whether the vacation was updated successfully or an error response.
    /// </returns>
  
    [HttpPut]
    public async Task<IActionResult> UpdateVacation([FromForm] UpdateVacationRequest request)
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
            await _repos.UpdateVacationAsync(request);
            return Ok(new { message = "Vacation updated successfully." });
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
    /// Deletes a vacation by its ID.
    /// </summary>
    /// <param name="id">The ID of the vacation to delete.</param>
    /// <returns>
    /// A message indicating whether the vacation was deleted successfully or an error response.
    /// </returns>
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacation(int id)
    {
        try
        {
            await _repos.DeleteVacationAsync(id);
            return Ok(new { message = "Vacation deleted successfully." });
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
