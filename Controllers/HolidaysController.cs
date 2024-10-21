using HumanResourcesWebApi.Models.Requests.Holidays;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController(IHolidaysRepository repos) : ControllerBase
    {
        private readonly IHolidaysRepository _repos = repos;

        /// <summary>
        /// Retrieves holidays for a specified year, or for the current year if no year is provided.
        /// </summary>
        /// <param name="year">The optional year for which to retrieve holidays. If not specified, holidays for the current year will be retrieved.</param>
        /// <returns>A list of holidays for the specified year, or a 404 if no holidays are found.</returns>
        [HttpGet("{year?}")]
        public async Task<IActionResult> GetHolidays(int? year)
        {
            try
            {
                var result = await _repos.GetHolidaysAsync(year);

                if (result == null || result?.Count == 0)
                    return NotFound(new { message = $"No holidays found for the year {year ?? DateTime.Now.Year}." });

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

        /// <summary>
        /// Adds a new holiday to the database.
        /// </summary>
        /// <param name="request">The details of the holiday to be added.</param>
        /// <returns>A success message if the holiday is added, or an error if validation fails or a conflict occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> AddHoliday([FromForm] AddHolidayRequest request)
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
                await _repos.AddHolidayAsync(request);
                return Ok(new { message = "Holiday added successfully." });
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

        /// <summary>
        /// Updates an existing holiday.
        /// </summary>
        /// <param name="request">The updated holiday details.</param>
        /// <returns>A success message if the holiday is updated, or an error if validation fails or a conflict occurs.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateHoliday([FromForm] UpdateHolidayRequest request)
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
                await _repos.UpdateHolidayAsync(request);
                return Ok(new { message = "Holiday updated successfully." });
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

        /// <summary>
        /// Deletes a holiday by its ID.
        /// </summary>
        /// <param name="id">The ID of the holiday to delete.</param>
        /// <returns>A success message if the holiday is deleted, or an error if a server error occurs.</returns>
        [HttpDelete]

        public async Task<IActionResult> DeleteHoldiday(int id)
        {
            try
            {
                await _repos.DeleteHolidayAsync(id);
                return Ok(new { message = "Holiday deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the award.", details = ex.Message });
            }
        }
    }
}
