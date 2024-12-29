using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Rights;
using HumanResourcesWebApi.Models.Requests.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesRepository _repos;
    public RolesController(IRolesRepository repos) => _repos = repos;


    #region Add
    /// <summary>
    /// Add a new Role 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> AddRole([FromForm] AddRoleRequest request)
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
            await _repos.AddRoleAsync(request);
            return Ok(new { message = "Role added successfully." });
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




    #endregion

    #region Get

    /// <summary>
    /// Get a list of Roles
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var roles = await _repos.GetAllRolesAsync();
            if (roles == null || roles.Count <= 0)
            {
                return NotFound(new { message = "No roles found " });
            }
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Update

    /// <summary>
    /// Update a Role by its Id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleRequest request)
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
            await _repos.UpdateRoleAsync(request);
            return Ok(new { message = "Role updated successfully." });
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

    #endregion
}
