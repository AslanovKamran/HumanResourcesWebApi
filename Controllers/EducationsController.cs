using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.Educations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationRepository _repos;
        public EducationsController(IEducationRepository repos) => _repos = repos;


        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeEducation(int employeeId)
        {
            if (employeeId <= 0)
                return BadRequest(new { message = "Invalid employee ID." });

            try
            {
                var result = await _repos.GetEmployeeEducationAsync(employeeId);

                if (result == null || result.Count == 0)
                    return NotFound(new { message = "No education records found for this employee." });

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
        public async Task<IActionResult> AddEmployeeEducation([FromForm] AddEmployeeEducationRequest education) 
        {
                if (!ModelState.IsValid)
                    return BadRequest(new { model = education });
            try
            {
                await _repos.AddEmployeeEducationAsync(education);
                return Ok(new { message = "Education Added Successfully!", addedEducation = education });
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
        public async Task<IActionResult> UpdateEducation([FromForm] UpdateEmployeeEducationRequest education) 
        {
            if(!ModelState.IsValid)
                return BadRequest(new { model = education });

            try
            {
                await _repos.UpdateEmployeeEducationAsync(education);
                return Ok(new { message="Education Updated Successfully!", updatedEducation = education });
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
        public async Task<IActionResult> DeleteEmployeeEducation(int id)
        {
            try
            {
                await _repos.DeleteEmployeeEducationAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new { message = "Education deleted successfully!" });
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
