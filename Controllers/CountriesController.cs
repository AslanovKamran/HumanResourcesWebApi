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
}
