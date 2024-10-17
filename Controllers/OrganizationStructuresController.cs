using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HumanResourcesWebApi.Models.Requests.OrganizationStructures;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationStructuresController : ControllerBase
    {
        private readonly IOrganizationStructuresRepository _repos;

        public OrganizationStructuresController(IOrganizationStructuresRepository repos) => _repos = repos;

        [HttpGet]
        public async Task<IActionResult> GetAllOrganizations([FromQuery] bool includeCanceled = false)
        {
            var organizationStructures = await _repos.GetOrganizationStructureListAsync(includeCanceled);
            return Ok(organizationStructures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(int id)
        {
            var organizationStructure = await _repos.GetOrganizationStructureByIdAsync(id);
            return Ok(organizationStructure);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganizationStructure([FromForm] AddOrganizationStructureRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var addedOrganization = await _repos.AddOrganizationStructureAsync(request);
                return Ok(addedOrganization);
            }

            catch (SqlException ex)
            {
                return Conflict($"SQL Exception:\n Error Code: {ex.ErrorCode}\nError Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrganizationStructure([FromForm] UpdateOrganizationStructureRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _repos.UpdateOrganizationStrcuture(request);
                return Ok(updated);
            }
            catch (SqlException ex)
            {
                
                return Conflict($"SQL Exception:\n Error Code: {ex.ErrorCode}\nError Message: {ex.Message}");
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteOrganizationStructure(int id)
        {
            try
            {
                await _repos.DeleteOrganizationStructureAsync(id);
                return Ok(new { Message = "Organization structure successfully marked as canceled." });
            }
            catch (SqlException ex)
            {
                return Conflict(new { Message = $"SQL Exception: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An error occurred: {ex.Message}" });
            }
        }


    }
}
