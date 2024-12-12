using HumanResourcesWebApi.Models.Requests.TabelAbsent;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabelAbsentController : ControllerBase
{
    private readonly ITabelAbsentRepository _repos;

    public TabelAbsentController(ITabelAbsentRepository repos) => _repos = repos;

    #region Get

    /// <summary>
    /// Get Absents by Employee Id and Year (leave blank for the current year)
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int employeeId, [FromQuery] int? year)
    {
        try
        {
            var date = year ?? DateTime.Now.Year;
            var result = await _repos.GetTabelAbsentsByIdAsync(employeeId, date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Add new Tabel Absent entry for the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddTabelAbsentRequest request)
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
            await _repos.AddTabelAbsentAsync(request);
            return Ok(new { message = "Tabel absent added successfully." });
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
    /// Update Tabel Absent entry of the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateTabelAbsentRequest request)
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
            await _repos.UpdateTabelAbsentAsync(request);
            return Ok(new { message = "Tabel absent updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the vacation.", details = ex.Message });
        }
    }


    #endregion

    #region Delete

    /// <summary>
    /// Delete an individual Tabel Absent entry
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteTabelAbsentAsync(id);
            return Ok(new { message = "Tabel absent deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the tabel absent.", details = ex.Message });
        }
    }

    #endregion

}
