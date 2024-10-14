using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Vacations;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationsRepository _repos;

        public VacationsController(IVacationsRepository repos) => _repos = repos;


        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetVacations(int employeeId, int? yearStarted = null, int? yearEnded = null)
        {
            try
            {
                var vacations = await _repos.GetVacationsAsync(employeeId, yearStarted, yearEnded);
                if (vacations == null || !vacations.Any())
                {
                    return NotFound($"No vacations found for employee ID: {employeeId}");
                }
                return Ok(vacations);
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
        public async Task<IActionResult> AddVacation([FromForm] AddVacationRequest request)
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
                return Ok(new { message = "Vacation added successfully." });
            }
            catch (SqlException ex)
            {
                return Conflict(new { message = "Database conflict occurred", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Optional logging here
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVacation( [FromForm] UpdateVacationRequest request)
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
                return Ok(new { message = "Vacation updated successfully." });
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacation(int id)
        {
            try
            {
                await _repos.DeleteVacationAsync(id);
                return Ok(new { message = "Vacation deleted successfully." });
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
}
