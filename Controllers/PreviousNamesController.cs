using HumanResourcesWebApi.Models.Requests.PreviousNames;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviousNamesController : ControllerBase
    {
        private readonly IPreviousNamesRepository _repos;
        public PreviousNamesController(IPreviousNamesRepository repos) => _repos = repos;

        /// <summary>
        /// Retrieves a list of previous names for a given employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
       
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetPreviousNames(int employeeId)
        {
            try
            {
                var result = await _repos.GetPreviousNamesAsync(employeeId);
                if (result is null || result.Count == 0)
                    return NotFound(new { message = "No previous names found for this employee." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
            }
        }
        /// <summary>
        /// Adds a new previous name for an employee.
        /// </summary>
        /// <param name="request">The request containing the previous name details.</param>
        /// <returns>Returns success message on successful creation.</returns>
        [HttpPost]
        public async Task<IActionResult> AddPreviousName([FromForm] AddPreviousNameRequest request)
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
                await _repos.AddPreviousNameAsync(request);
                return Ok(new { message = "Previous name added successfully." });
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
        /// Updates an existing previous name for an employee.
        /// </summary>
        /// <param name="request">The request containing the updated previous name details.</param>
        /// <returns>Returns success message on successful update.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePreviousName([FromForm] UpdatePreviousNameRequest request)
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
                await _repos.UpdatePreviousNameAsync(request);
                return Ok(new { message = "Previous name updated successfully." });
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
        /// Deletes an existing previous name for an employee.
        /// </summary>
        /// <param name="id">The ID of the previous name to delete.</param>
        /// <returns>Returns success message on successful deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreviousName(int id)
        {
            try
            {
                await _repos.DeletePreviousNameAsync(id);
                return Ok(new { message = "Previous name deleted successfully." });
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
