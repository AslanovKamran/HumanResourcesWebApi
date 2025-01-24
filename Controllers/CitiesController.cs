using HumanResourcesWebApi.Models.Requests.Cities;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController(ICitiesRepository repos) : ControllerBase
{
    private readonly ICitiesRepository _repos = repos;
  
    #region Get

    /// <summary>
    /// Retrieves a paginated list of cities.
    /// </summary>
    /// <param name="itemsPerPage">Number of items to display per page. Default is 50.</param>
    /// <param name="currentPage">The current page to display. Default is 1.</param>
    /// <returns>
    /// A list of cities and associated pagination information.
    /// </returns>

    [HttpGet]
    public async Task<IActionResult> GetCities([FromQuery] int itemsPerPage = 50, [FromQuery] int currentPage = 1)
    {
        try
        {
            var (cities, pageInfo) = await _repos.GetCitiesAsync(itemsPerPage, currentPage);
            return Ok(new
            {
                Cities = cities,
                PageInfo = pageInfo
            });
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
    /// Adds a new city.
    /// </summary>
    /// <param name="request">The request object containing the details of the new city.</param>
    /// <returns>
    /// A success message if the city is added, or a validation error or server error response.
    /// </returns>
   
    [HttpPost]
    public async Task<IActionResult> AddCity([FromForm] AddCityRequest request)
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
            await _repos.AddCityAsync(request);
            return Ok(new { message = "City added successfully." });
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
    /// Updates an existing city.
    /// </summary>
    /// <param name="request">The request object containing the updated city details.</param>
    /// <returns>
    /// A success message if the city is updated, or a validation error or server error response.
    /// </returns>
    /// <response code="200">City updated successfully.</response>
    /// <response code="400">Validation failed for the input request.</response>
    /// <response code="409">Database conflict occurred.</response>
    /// <response code="500">An error occurred while processing the request.</response>

    [HttpPut]
    public async Task<IActionResult> UpdateCity([FromForm] UpdateCityRequest request)
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
            await _repos.UpdateCityAsync(request);
            return Ok(new { message = "City updated successfully." });
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
    /// Deletes a city by its ID.
    /// </summary>
    /// <param name="id">The ID of the city to be deleted.</param>
    /// <returns>
    /// A success message if the city is deleted, or a conflict or server error response.
    /// </returns>
   
    [HttpDelete]
    public async Task<IActionResult> DeleteCity(int id) 
    {
        try
        {
            await _repos.DeleteCityAsync(id);
            return Ok("City has been deleted successfully");
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
