using HumanResourcesWebApi.Models.Requests.BusinessTrips;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Azure.Core;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessTripsController(IBusinessTripsRepository repos) : ControllerBase
{
    private readonly IBusinessTripsRepository _repos = repos;

    #region Get
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

    #endregion

    #region Add


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

    [HttpPost]
    [Route("employee")]
    public async Task<IActionResult> AddEmployeeToBusinessTrip(int tripId, int employeeId)
    {
        try
        {
            await _repos.AddEmployeeToBusinessTripAsync(tripId, employeeId);
            return Ok(new { message = "Employee has been successfully added to the trip." });
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
    [Route("destination")]
    public async Task<IActionResult> AddDestinationPointToBusinessTrip(int tripId, int cityId, string destinationPoint)
    {
        try
        {
            await _repos.AddDestinationPointToBusinessTripAsync(tripId, cityId, destinationPoint);
            return Ok(new { message = "Destination has been successfully added to the trip." });
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

    #endregion

    [HttpDelete("employee/{id}")]
    public async Task<IActionResult> RemoveEmployeeFromBusinessTrip(int id) 
    {
        try
        {
            await _repos.RemoveEmployeeFromBusinessTripAsync(id);
            return Ok(new { message = "Employee has been successfully deleted from the trip." });
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

    [HttpDelete("destination/{id}")]
    public async Task<IActionResult> RemoveDestinationFromBusinessTrip(int id)
    {
        try
        {
            await _repos.RemoveDestinationPointFromBusinessTripAsync(id);
            return Ok(new { message = "Destination Point has been successfully deleted from the trip." });
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

    #region Update

    [HttpPut]
    
    public async Task<IActionResult> UpdateBusinessTrip(UpdateBusinessTripRequest request)
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
            await _repos.UpdateBusinessTripAsync(request);
            return Ok(new { message = "Trip has been successfully updated" });
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
    [Route("destination")]
    public async Task<IActionResult> UpdateDestinationPointOfBusinessTrip(int entryId, int cityId, string destinationPoint)
    {
        try
        {
            await _repos.UpdateDestinationPointOfBusinessTripAsync(entryId,cityId,destinationPoint);
            return Ok(new { message = "Destination Point has been successfully updated for the trip." });
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
