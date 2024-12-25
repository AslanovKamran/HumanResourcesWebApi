using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabelController : ControllerBase
{
    private readonly ITabelRepository _repos;
    public TabelController(ITabelRepository repos) => _repos = repos;

    [HttpGet]
    public async Task<IActionResult> GetTabel(int year, int month, int? organizationStructureId) 
    {
        try
        {
            var result = await _repos.GetTabelDataAsync(year, month, organizationStructureId);  
            return Ok(result);

        }
        catch (SqlException ex)
        {
            return StatusCode(409, new { message = "SQL Error: ", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred: ", details = ex.Message });
        }
    }
}
