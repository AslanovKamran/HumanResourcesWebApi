using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.BusinessTrips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessTripsController(IBusinessTripsRepository repos) : ControllerBase
{
    private readonly IBusinessTripsRepository _repos = repos;

    [HttpGet("{tripId}")]
    public async Task<IActionResult> GetBusinessTripDetails(int tripId)
    {
        try
        {
            // Call the repository method to get business trip details
            var businessTripDetails = await _repos.GetBusinessTripDetailsAsync(tripId);

            if (businessTripDetails == null)
            {
                return NotFound(new { message = $"Business trip with ID {tripId} not found." });
            }

            // Return the result as a successful response
            return Ok(businessTripDetails);
        }
        catch (SqlException ex)
        {
            // Handle database-related exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred.", details = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBusinessTrips([FromQuery] int itemsPerPage = 20, [FromQuery] int currentPage = 1)
    {

        try
        {
            var result = await _repos.GetBusinessTrips(itemsPerPage, currentPage);
            return Ok(new
            {
                Data = result.BusinessTrips,
                PageInfo = result.PageInfo
            });
        }
        catch (SqlException ex)
        {
            // Handle database-related exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred.", details = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddBusinessTrip([FromForm] AddBusinessTripWithDetailsRequest request)
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
            await _repos.AddBusinessTripWithDetailsAsync(request);
            return Ok(new { message = "Business trip added successfully." });
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
