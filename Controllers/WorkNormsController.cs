using HumanResourcesWebApi.Models.Requests.WorkNorms;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkNormsController(IWorkNormsRepository repos) : ControllerBase
    {
        private readonly IWorkNormsRepository _repos = repos;

        [HttpGet("{year?}")]
        public async Task<IActionResult> GetWorkNorms(int? year) 
        {
            try
            {
                var result = await _repos.GetWorkNormsAsync(year);
                if (result == null || result.Count ==0)
                {
                    return NotFound($"No work norms found for the {year ?? DateTime.Now.Year} year");
                }
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
        public async Task<IActionResult> AddWorkNorm([FromForm] AddWorkNormRequest request) 
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
                await _repos.AddWorkNormAsync(request);
                return Ok(new { message = "Work norm added successfully." });
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
        public async Task<IActionResult> UpdateWorkNorm([FromForm] UpdateWorkNormRequest request) 
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
                await _repos.UpdateWorkNormAsync(request);
                return Ok(new { message = "Work norm updated successfully." });
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
        public async Task<IActionResult> DeleteWorkNorm(int id)
        {
           
            try
            {
                await _repos.DeleteWorkNormAsync(id);
                return Ok(new { message = "Work norm deleted successfully." });
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
