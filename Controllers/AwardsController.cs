using HumanResourcesWebApi.Models.Requests.Awards;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController(IAwardsRepository repos) : ControllerBase
    {
        private readonly IAwardsRepository _repos = repos;

        /// <summary>
        /// Retrieves a list of awards for a specific employee by their ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee whose awards are being retrieved.</param>
        /// <returns>A list of awards associated with the specified employee, or a 404 if none are found.</returns>
        
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAwards(int employeeId)
        {
            try
            {
                var awards = await _repos.GetAwardsAsync(employeeId);
                if (awards == null || awards.Count == 0)
                {
                    return NotFound(new { message = "No awards found for the specified employee." });
                }
                return Ok(awards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new award for an employee.
        /// </summary>
        /// <param name="request">The details of the award to be added.</param>
        /// <returns>A success message if the award is added, or a validation or conflict error if an issue occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> AddAward([FromForm] AddAwardRequest request)
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
                await _repos.AddAwardAsync(request);
                return Ok(new { message = "Award added successfully." });
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
        /// Updates an existing award based on the provided details.
        /// </summary>
        /// <param name="request">The updated award details.</param>
        /// <returns>A success message if the update is successful, or an error message if the update fails.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAward([FromForm] UpdateAwardRequest request)
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
                await _repos.UpdateAwardAsync(request);
                return Ok(new { message = "Award updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the award.", details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an award by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the award to delete.</param>
        /// <returns>A success message if the deletion is successful, or an error message if the deletion fails.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            try
            {
                await _repos.DeleteAwardAsync(id);
                return Ok(new { message = "Award deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the award.", details = ex.Message });
            }
        }
    }
}
