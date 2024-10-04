using HumanResourcesWebApi.Models.Requests;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Abstract;
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
        public async Task<IActionResult> GetChunk([FromQuery] EmployeeFilter filter, [FromQuery] int itemsPerPage = 10, [FromQuery] int currentPage = 1)
        {
            try
            {
                var result = await _repos.GetEmployeesChunkAsync(filter, itemsPerPage, currentPage);

                foreach (var item in result.Employees) 
                {
                    Console.WriteLine($"{item.Surname} {item.Name}" );
                }

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

        [HttpGet("generalInfo")]
        public async Task<IActionResult> GetEmployeeGeneralInfo([FromQuery] int id)
        {
            try
            {
                var employee = await _repos.GetEmployeeGeneralInfoAsync(id);

                if (employee == null)
                {
                    return NotFound(new { message = "Employee not found" });
                }

                return Ok(employee);
            }
            catch (SqlException ex)
            {
                // Returning 409 Conflict for database conflicts
                return Conflict(new { message = "Database conflict occurred", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
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
