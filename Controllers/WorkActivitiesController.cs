using HumanResourcesWebApi.Models.Requests.WorkActivities;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkActivitiesController(IWorkActivitiesRepository repos) : ControllerBase
{
    private readonly IWorkActivitiesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves all work activities for a specific employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve work activities for.</param>
    /// <returns>
    /// A list of work activities for the specified employee, or an error message if the retrieval fails.
    /// </returns>
    
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeeWorkActivities(int employeeId) 
    {
		try
		{
			var result = await _repos.GetEmployeeWorkActivityAsync(employeeId);
			return Ok(result);	
		}
        catch (SqlException ex)
        {
            return Conflict(new { message = "An error occurred while fetching the data", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new work activity.
    /// </summary>
    /// <param name="request">The request object containing details of the work activity to add.</param>
    /// <returns>
    /// A message indicating whether the work activity was added successfully or an error response.
    /// </returns>

    [HttpPost]
    public async Task<IActionResult> AddWorkActivity([FromForm] AddWorkActivityRequest request)
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
            await _repos.AddWorkActivityAsync(request);
            return Ok(new { message = $"Work activity with Id {request.Id} added successfully!" });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "An error occurred while adding the work activity.", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }


    #endregion

    #region Update

    /// <summary>
    /// Updates an existing work activity.
    /// </summary>
    /// <param name="request">The request object containing the updated details of the work activity.</param>
    /// <returns>
    /// A message indicating whether the work activity was updated successfully or an error response.
    /// </returns>
    
    [HttpPut]
    public async Task<IActionResult> UpdateWorkActivity([FromForm] UpdateWorkActivityRequest request)
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
            await _repos.UpdateWorkActivityAsync(request);
            return Ok(new { message = $"Work acitvity with Id {request.Id} updated successfully!" });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "An error occurred while updating the work activity.", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes a work activity by its ID.
    /// </summary>
    /// <param name="id">The ID of the work activity to delete.</param>
    /// <returns>
    /// A message indicating whether the work activity was deleted successfully or an error response.
    /// </returns>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkActivity(int id)
    {
        try
        {
            await _repos.DeleteWorkActivityAsync(id);
            return Ok(new { message = $"Work activity with Id {id} deleted successfully!" });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "An error occurred while deleting the work activity.", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    #endregion

}
