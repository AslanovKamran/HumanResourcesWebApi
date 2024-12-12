using HumanResourcesWebApi.Models.Requests.TabelBulletin;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabelBulletinController : ControllerBase
{
    private readonly ITabelBulletinRepository _repos;
    public TabelBulletinController(ITabelBulletinRepository repos) => _repos = repos;

    #region Get

    /// <summary>
    /// Get Bulletins by Employee Id and Year Boundaries
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
            beginYear ??= DateTime.Now.Year;
            endYear ??= beginYear + 4;

            var result = await _repos.GetTabelBulletinsAsync(employeeId, beginYear, endYear);
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
    /// Add new Tabel Bulletin entry for the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddTabelBulletinRequest request)
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
            await _repos.AddTabelBulletinAsync(request);
            return Ok(new { message = "Tabel bulletin updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the bulletin.", details = ex.Message });
        }
    }

    #endregion

    #region Update

    /// <summary>
    /// Update Tabel Bulletin entry of the specific Employee
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// 
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateTabelBulletinRequest request)
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
            await _repos.UpdateTabelBulletinAsync(request);
            return Ok(new { message = "Tabel bulletin updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the bulletin.", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an individual Tabel Bulletin entry
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repos.DeleteTabelBulletinByIdAsync(id);
            return Ok(new { message = "Tabel bulletin deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the tabel bulletin.", details = ex.Message });
        }
    }

    #endregion
}
