using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Brigades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrigadesController : ControllerBase
{
    private readonly IBrigadesRepository _repos;
    public BrigadesController(IBrigadesRepository repos) => _repos = repos;


    /// <summary>
    /// Get a list of brigades
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var brigades = await _repos.GetBrigadesAsync();
            return Ok(brigades);
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
    /// Get employees by the brigade Id
    /// </summary>
    /// <param name="brigadeId"></param>
    /// <returns></returns>
    
    [HttpGet("brigades/{brigadeId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetByBrigade(int brigadeId)
    {
        try
        {
            var result  = await _repos.GetEmployeesByBrigadeId(brigadeId);
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


    /// <summary>
    /// Get individual brigade by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var brigade = await _repos.GetBrigadeByIdAsync(id);
            return Ok(brigade);
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
    /// Add a new  brigade
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Add([FromForm] AddBrigadeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = ModelState
                .Where(v => v.Value!.Errors.Any())
                .Select(v => new { Field = v.Key, Errors = v.Value!.Errors.Select(e => e.ErrorMessage) })
            });
        }

        try
        {
            await _repos.AddBrigadeAsync(request);
            return Ok(new { message = "Brigade added successfully." });
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
    /// Update an existing brigade
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update([FromForm] UpdateBrigadeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = ModelState
                .Where(v => v.Value!.Errors.Any())
                .Select(v => new { Field = v.Key, Errors = v.Value!.Errors.Select(e => e.ErrorMessage) })
            });
        }

        try
        {
            await _repos.UpdateBrigadeAsync(request);
            return Ok(new { message = "Brigade updated successfully." });
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
    /// Delete an existing brigade
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteBrigadeAsync(id);
            return Ok("The brigade has been deleted successfully");
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
    /// Assign a new brigade to the employee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="brigadeId"></param>
    /// <returns></returns>

    [HttpPost]
    [Route("assing")]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Assign(int employeeId, int brigadeId)
    {
        try
        {
            await _repos.AssignNewBrigadeToEmployeeAsync(employeeId, brigadeId);
            return Ok("The brigade has been assigned successfully");
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
    /// Remove all brigades of the employee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>

    [HttpPost]
    [Route("unassing")]
    [ProducesResponseType(200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Unassign(int employeeId)
    {
        try
        {
            await _repos.UnassignFromAllBrigades(employeeId);
            return Ok("All brigades of the employee has been removed");
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




}
