using HumanResourcesWebApi.Models.Requests.FamilyMembers;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase
    {
        private readonly IFamilyMembersRepository _repos;
        public FamilyMembersController(IFamilyMembersRepository repos) => _repos = repos;

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetFamilyMembers(int employeeId)
        {
            try
            {
                var result = await _repos.GetFamilyMembersAsync(employeeId);
                if (result is null || result.Count == 0)
                    return NotFound(new { message = "No family members found for this Employee" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFamilyMember([FromForm] AddFamilyMemberRequest request)
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
                await _repos.AddFamilyMemberAsync(request);
                return Ok(new { message = "Family Member added successfully." });
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
        public async Task<IActionResult> UpdateFamilyMember([FromForm] UpdateFamilyMemberRequest request) 
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
                await _repos.UpdateFamilyMemberAsync(request);
                return Ok(new { message = "Family Member updated successfully." });
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
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {
            try
            {
                await _repos.DeleteFamilyMemberAsync(id);
                return Ok(new { message = "Family member deleted successfully." });
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
