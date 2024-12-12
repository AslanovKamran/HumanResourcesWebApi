using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.TabelBulletin;
using HumanResourcesWebApi.Models.Requests.TabelExtraWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabelExtraWorkController : ControllerBase
{
    private readonly ITabelExtraWorkRepository _repos;
    public TabelExtraWorkController(ITabelExtraWorkRepository repos) => _repos = repos;

    #region Get

    /// <summary>
    /// Get Extra works by Employee Id and Year (leave blank for the current year)
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(int employeeId, int? year) 
    {
        year = year ?? DateTime.Now.Year;

        try
        {
            var result = await _repos.GetExtraWorkAsync(employeeId, year.Value);
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
    /// Add new Tabel Extra work entry for the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddTabelExtraWorkRequest request) 
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
            await _repos.AddExtraWorkAsync(request);
            return Ok(new { message = "Tabel extra work added successfully." });
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
    /// Update Tabel Extra work of the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// 
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateTabelExtraWorkRequest request)
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
            await _repos.UpdateExtraWorkAsync(request);
            return Ok(new { message = "Tabel extra work updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the extra work.", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an individual Tabel extra work entry
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteExtraWorkByIdAsync(id);
            return Ok(new { message = "Tabel extra work deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the tabel extra work.", details = ex.Message });
        }
    }

    #endregion
}
