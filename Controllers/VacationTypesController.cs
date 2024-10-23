using HumanResourcesWebApi.Models.Requests.VacationTypes;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationTypesController(IVacationTypesRepository repos) : ControllerBase
{
    private readonly IVacationTypesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves all vacation types.
    /// </summary>
    /// <returns>
    /// A list of vacation types, or a message indicating no types were found.
    /// </returns>

    [HttpGet]
    public async Task<IActionResult> GetVacationTypes()
    {
        try
        {
            var result = await _repos.GetVacationTypesAsync();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No vacation types found " });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new vacation type.
    /// </summary>
    /// <param name="request">The request object containing the details of the vacation type to add.</param>
    /// <returns>
    /// A message indicating whether the vacation type was added successfully or an error response.
    /// </returns>
    
    [HttpPost]
    public async Task<IActionResult> AddVacationType([FromForm] AddVacationTypeRequest request)
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
            return Ok(new { message = "Vacation type added successfully." });
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
    /// Updates an existing vacation type.
    /// </summary>
    /// <param name="request">The request object containing the updated vacation type details.</param>
    /// <returns>
    /// A message indicating whether the vacation type was updated successfully or an error response.
    /// </returns>

    [HttpPut]
    public async Task<IActionResult> UpdateAward([FromForm] UpdateVacationTypeRequest request)
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
            return Ok(new { message = "Vacation type updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the vacation.", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes a vacation type by its ID.
    /// </summary>
    /// <param name="id">The ID of the vacation type to delete.</param>
    /// <returns>
    /// A message indicating whether the vacation type was deleted successfully or an error response.
    /// </returns>
   
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAward(int id)
    {
        try
        {
            await _repos.DeleteVacationAsync(id);
            return Ok(new { message = "Vacation deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the vacation.", details = ex.Message });
        }
    }

    #endregion

}
