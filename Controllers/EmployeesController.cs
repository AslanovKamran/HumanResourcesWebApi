using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _repos;

        public EmployeesController(IEmployeesRepository repos) => _repos = repos;

        [HttpGet]
        public async Task<IActionResult> GetChunk(int itemsPerPage = 10, int currentPage = 1) 
        {
            try
            {
                var result = await _repos.GetEmployeesChunkAsync(itemsPerPage, currentPage);

                return Ok(new
                {
                    Data = result.Employees,
                    PageInfo = result.PageInfo
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
