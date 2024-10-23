using HumanResourcesWebApi.Models.Requests.Countries;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesRepository repos) : ControllerBase
{
    private readonly ICountriesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a list of all countries.
    /// </summary>
    /// <returns>
    /// A list of countries.
    /// </returns>
    
    [HttpGet]

    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var result = await _repos.GetCountriesAsync();
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

    #endregion

    #region Add

    /// <summary>
    /// Adds a new country.
    /// </summary>
    /// <param name="request">The request object containing the details of the new country.</param>
    /// <returns>
    /// A success message if the country is added, or a validation error or server error response.
    /// </returns>

    [HttpPost]
    public async Task<IActionResult> AddCountry([FromForm] AddCountryRequest request)
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
            await _repos.AddCountryAsync(request);
            return Ok(new { message = "Country added successfully." });
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
    /// Updates an existing country.
    /// </summary>
    /// <param name="request">The request object containing the updated country details.</param>
    /// <returns>
    /// A success message if the country is updated, or a validation error or server error response.
    /// </returns>

    [HttpPut]
    public async Task<IActionResult> UpdateCity([FromForm] UpdateCountryRequest request)
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
            await _repos.UpdateCountryAsync(request);
            return Ok(new { message = "Country updated successfully." });
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
    /// Deletes a country by its ID.
    /// </summary>
    /// <param name="id">The ID of the country to be deleted.</param>
    /// <returns>
    /// A success message if the country is deleted, or a conflict or server error response.
    /// </returns>

    [HttpDelete]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            await _repos.DeleteCountryAsync(id);
            return Ok("Country has been deleted successfully");
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
