using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Medals;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedalsController : ControllerBase
    {
        private readonly IMedalsRepository _repos;
        public MedalsController(IMedalsRepository repos) => _repos = repos;

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetMedals(int employeeId)
        {
            try
            {
                var medals = await _repos.GetMedalsAsync(employeeId);
                if (medals == null || medals.Count == 0)
                {
                    return NotFound(new { message = "No medals found for the specified employee." });
                }
                return Ok(medals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedal([FromForm] AddMedalRequest request)
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
                await _repos.AddMedalAsync(request);
                return Ok(new { message = "Medal added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the medal.", details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedal([FromForm] UpdateMedalRequest request)
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
                await _repos.UpdateMedalAsync(request);
                return Ok(new { message = "Medal updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the medal.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedal(int id)
        {
            try
            {
                await _repos.DeleteMedalAsync(id);
                return Ok(new { message = "Medal deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the medal.", details = ex.Message });
            }
        }

    }
}
