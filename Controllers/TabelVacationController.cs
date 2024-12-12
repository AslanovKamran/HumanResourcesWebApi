using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.TabelAbsent;
using HumanResourcesWebApi.Models.Requests.TabelVacation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabelVacationController : ControllerBase
{
    private readonly ITabelVacationRepository _repos;
    public TabelVacationController(ITabelVacationRepository repos) => _repos = repos;

    #region Get

    /// <summary>
    /// Get Vacations by Employee Id and Year Boundaries
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="beginYear"></param>
    /// <param name="endYear"></param>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> Get(int employeeId, int? beginYear, int? endYear)
    {
        try
        {
            var beginDate = beginYear ?? DateTime.Now.Year - 1;
            var endDate = endYear ?? beginDate + 4;

            var result = await _repos.GetTabelVacationsAsync(employeeId, beginDate, endDate);
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
    /// Add new Tabel Vacation entry for the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddTabelVacationRequest request) 
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
            await _repos.AddTabelVacationAsync(request);
            return Ok(new { message = "Tabel vacation added successfully." });
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
    public async Task<IActionResult> Update([FromForm] UpdateTabelVacationRequest request)
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
            await _repos.UpdateTabelVacationAsync(request);
            return Ok(new { message = "Tabel vacation updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the vacation.", details = ex.Message });
        }
    }


    #endregion

    #region Delete

    /// <summary>
    /// Delete an individual Tabel Vacation entry
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteTabelVacationAsync(id);
            return Ok(new { message = "Tabel vacation deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the tabel vacation.", details = ex.Message });
        }
    }

    #endregion
}
