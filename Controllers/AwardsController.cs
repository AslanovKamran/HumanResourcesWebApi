using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Awards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardsRepository _repos;

        public AwardsController(IAwardsRepository repos)
        {
            _repos = repos;
        }

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

        // POST: api/awards
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

        // PUT: api/awards
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

        // DELETE: api/awards/{id}
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
