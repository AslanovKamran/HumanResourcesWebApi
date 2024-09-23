using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationStructuresController : ControllerBase
    {
        private readonly IOrganizationStructuresRepository _repos;
        private readonly ILogger<OrganizationStructuresController> _logger; // Add this
        public OrganizationStructuresController(IOrganizationStructuresRepository repos, ILogger<OrganizationStructuresController> logger)
        {
            _repos = repos;
            _logger = logger;

        }

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
        public async Task<IActionResult> AddOrganization([FromForm] AddOrganizationStructureRequest request)
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

    }
}
