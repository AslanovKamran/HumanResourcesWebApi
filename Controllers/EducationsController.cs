using HumanResourcesWebApi.Models.Requests.Educations;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController(IEducationRepository repos) : ControllerBase
    {
        private readonly IEducationRepository _repos = repos;

        /// <summary>
        /// Retrieves a list of education records for a specific employee by their ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee whose education records are being retrieved.</param>
        /// <returns>A list of education records associated with the specified employee, or an error if no records are found or a conflict occurs.</returns>
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

        /// <summary>
        /// Adds a new education record for an employee.
        /// </summary>
        /// <param name="education">The details of the education record to be added.</param>
        /// <returns>A success message if the education record is added, or an error if validation fails or a conflict occurs.</returns>
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


        /// <summary>
        /// Updates an existing education record for an employee.
        /// </summary>
        /// <param name="education">The updated education record details.</param>
        /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>
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

        /// <summary>
        /// Deletes an education record by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the education record to delete.</param>
        /// <returns>A success message if the deletion is successful, or an error if a conflict or server error occurs.</returns>
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
