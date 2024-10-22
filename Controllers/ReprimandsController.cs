using HumanResourcesWebApi.Models.Requests.Reprimands;
using HumanResourcesWebApi.Abstract;

using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ReprimandsController : ControllerBase
    {
        private readonly IReprimandsRepository _repos;
        public ReprimandsController(IReprimandsRepository repos) => _repos = repos;


        /// <summary>
        /// Retrieves a list of family members for a given employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>Returns a list of family members.</returns>
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetReprimands(int employeeId)
        {
            try
            {
                var result = await _repos.GetReprimandsAsync(employeeId);
                if (result == null || result.Count == 0)
                    return NotFound(new { message = "No reprimands found for the specified employee." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new family member for an employee.
        /// </summary>
        /// <param name="request">The request containing family member details.</param>
        /// <returns>Returns success message on successful creation.</returns>
        [HttpPost]
        public async Task<IActionResult> AddReprimand([FromForm] AddReprimandRequest request)
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
                await _repos.AddReprimandAsync(request);
                return Ok(new { message = "Reprimand added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the reprimand.", details = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing family member's details for an employee.
        /// </summary>
        /// <param name="request">The request containing updated family member details.</param>
        /// <returns>Returns success message on successful update.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateReprimand([FromForm] UpdateReprimandRequest request)
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
                await _repos.UpdateReprimandAsync(request);
                return Ok(new { message = "Reprimand updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the reprimand.", details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a family member for an employee.
        /// </summary>
        /// <param name="id">The ID of the family member to delete.</param>
        /// <returns>Returns success message on successful deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReprimand(int id)
        {
            try
            {
                await _repos.DeleteReprimandAsync(id);
                return Ok(new { message = "Reprimand deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the reprimand.", details = ex.Message });
            }
        }

    }
}
