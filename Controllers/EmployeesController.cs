using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _repos.AddEmployeeAsync(request);
                return Ok(new { message = "Employee added successfully." });
            }
            catch (SqlException ex)
            {
                return Conflict(new { message = "A database error occurred.", errorCode = ex.ErrorCode, errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An internal server error occurred.", errorMessage = ex.Message });
            }
        }

    }
}
