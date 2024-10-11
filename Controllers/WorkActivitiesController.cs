using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.WorkActivities;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkActivitiesController : ControllerBase
{
    private readonly IWorkActivitiesRepository _repos;

    public WorkActivitiesController(IWorkActivitiesRepository repos) => _repos = repos;


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
            return Ok(new { message = $"Work acitvity with Id {request.Id} updated successfully!"}); 
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


}
