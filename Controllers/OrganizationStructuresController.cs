using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationStructuresController : ControllerBase
    {
        private readonly IOrganizationStructuresRepository _repos;
        public OrganizationStructuresController(IOrganizationStructuresRepository repos) => _repos = repos;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeCanceled = false) 
        {
            var organizationStructures = await _repos.GetOrganizationStructureListAsync(includeCanceled);
            return Ok(organizationStructures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var organizationStructure = await _repos.GetOrganizationStructureByIdAsync(id);
            return Ok(organizationStructure);
        }


    }
}
