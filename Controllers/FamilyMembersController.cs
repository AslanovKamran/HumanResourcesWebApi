using HumanResourcesWebApi.Models.Requests.FamilyMembers;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FamilyMembersController(IFamilyMembersRepository repos) : ControllerBase
{
    private readonly IFamilyMembersRepository _repos = repos;

    #region Get

    /// <summary>
    /// Get All Family Members 
    /// </summary>
    /// <param name="itemsPerPage"></param>
    /// <param name="currentPage"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllFamilyMembers([FromQuery] int itemsPerPage = 10, [FromQuery] int currentPage = 1) 
    {
        try
        {
            var result = await _repos.GetAllFamilyMembers(itemsPerPage, currentPage);
            return Ok(new
            {
                Data = result.FamilyMembers,
                PageInfo = result.PageInfo
            });
        }
        catch (SqlException ex)
        {
            // Handle database-related exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred.", details = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves a list of family members for a specific employee by their ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose family members are being retrieved.</param>
    /// <returns>A list of family members associated with the specified employee, or a 404 if none are found.</returns>
    
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



    #endregion

    #region Add

    /// <summary>
    /// Adds a new family member for an employee.
    /// </summary>
    /// <param name="request">The details of the family member to be added.</param>
    /// <returns>A success message if the family member is added, or an error if validation fails or a conflict occurs.</returns>
   
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


    #endregion

    #region Update

    /// <summary>
    /// Updates an existing family member's information.
    /// </summary>
    /// <param name="request">The updated family member details.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>
  
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

    #endregion

    #region Delete
  
    /// <summary>
    /// Deletes a family member by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the family member to delete.</param>
    /// <returns>A success message if the deletion is successful, or an error if a conflict or server error occurs.</returns>
    
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

    #endregion

}
