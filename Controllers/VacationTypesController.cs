using HumanResourcesWebApi.Models.Requests.VacationTypes;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationTypesController(IVacationTypesRepository repos) : ControllerBase
    {
        private readonly IVacationTypesRepository _repos = repos;


        [HttpGet]
        public async Task<IActionResult> GetVacationTypes() 
        {
            try
            {
                var result = await _repos.GetVacationTypesAsync();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new { message = "No vacation types found " });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddVacationType([FromForm] AddVacationTypeRequest request)
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
                return Ok(new { message = "Vacation type added successfully." });
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
        public async Task<IActionResult> UpdateAward([FromForm] UpdateVacationTypeRequest request)
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
                return Ok(new { message = "Vacation type updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the vacation.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            try
            {
                await _repos.DeleteVacationAsync(id);
                return Ok(new { message = "Vacation deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the vacation.", details = ex.Message });
            }
        }
    }

}
