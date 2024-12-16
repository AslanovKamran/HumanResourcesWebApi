using HumanResourcesWebApi.Models.Requests.PoliticalParties;
using HumanResourcesWebApi.Models.Requests.Employees;
using HumanResourcesWebApi.Models.DTO.EmoloyeeDTO;
using HumanResourcesWebApi.Common.Filters;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HumanResourcesWebApi.Common.FileUploader;
using HumanResourcesWebApi.Common.FileEraser;
using HumanResourcesWebApi.Repository.Dapper;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IEmployeesRepository repos) : ControllerBase
{
    private readonly IEmployeesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a paginated list of employees based on the provided filter, items per page, and current page.
    /// </summary>
    /// <param name="filter">Filter criteria for employees.</param>
    /// <param name="itemsPerPage">Number of items to retrieve per page.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <returns>A paginated list of employees along with page info, or an error if the request fails.</returns>

    [HttpGet]
    public async Task<IActionResult> GetChunk([FromQuery] EmployeeFilter filter, [FromQuery] int itemsPerPage = 10, [FromQuery] int currentPage = 1)
    {
        try
        {
            var result = await _repos.GetEmployeesChunkAsync(filter, itemsPerPage, currentPage);
            return Ok(new
            {
                Data = result.Employees,
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
    /// Retrieves general information about a specific employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee whose information is being retrieved.</param>
    /// <returns>General information about the specified employee, or a 404 if not found.</returns>
    [HttpGet("generalInfo/{id}")]
    public async Task<IActionResult> GetEmployeeGeneralInfo(int id)
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
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", details = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves political party information for a specific employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee whose political party information is being retrieved.</param>
    /// <returns>Political party information for the specified employee, or a 404 if not found.</returns>
    [HttpGet("politicalParty/{id}")]
    public async Task<IActionResult> GetEmployeePoliticalPartyInfo(int id)
    {
        try
        {
            var politicalPartyInfo = await _repos.GetPoliticalPartyAsync(id);
            if (politicalPartyInfo == null || String.IsNullOrWhiteSpace(politicalPartyInfo.PoliticalParty))
            {
                return NotFound(new { message = "No political party information found for the specified employee." });
            }
            return Ok(politicalPartyInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }
    /// <summary>
    /// Retrieves military information for a specific employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee whose military information is being retrieved.</param>
    /// <returns>Military information for the specified employee, or a 404 if not found.</returns>
    [HttpGet("militaryInfo/{id}")]
    public async Task<IActionResult> GetMilitaryInfo(int id)
    {
        try
        {
            var militaryInfo = await _repos.GetEmployeeMilitaryInfoAsync(id);
            if (militaryInfo == null)
                return NotFound(new { message = "Military information not found for the specified employee." });

            return Ok(militaryInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new employee to the database.
    /// </summary>
    /// <param name="request">Details of the employee to be added.</param>
    /// <returns>A success message if the employee is added, or an error if validation fails or a conflict occurs.</returns>

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string photoUrl = string.Empty;
        try
        {
            if (request.ImageFile is not null)
            {
                request.PhotoUrl = FileUploader.UploadFile(request.ImageFile!);
                photoUrl = request.PhotoUrl;
            }
            await _repos.AddEmployeeAsync(request);

            return Ok(new { message = "Employee added successfully." });
        }
        catch (SqlException ex)
        {
            FileEraser.DeleteImage(photoUrl);
            return Conflict(new { message = "A database error occurred.", errorCode = ex.ErrorCode, errorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            FileEraser.DeleteImage(photoUrl);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An internal server error occurred.", errorMessage = ex.Message });
        }
    }

    #endregion

    #region Update

    /// <summary>
    /// Updates the general information for an existing employee.
    /// </summary>
    /// <param name="request">The updated employee information.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>

    [HttpPut("updateInfo")]
    public async Task<IActionResult> UpdateEmployeeGeneralInfo([FromForm] UpdateEmployeeGeneralInfoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

        try
        {
            await _repos.UpdateEmployeeGeneralInfoAsync(request);
            return Ok(new { message = "An employee has been successfully updated!" });
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

    /// <summary>
    /// Updates the political party information for an employee.
    /// </summary>
    /// <param name="request">The updated political party details.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>

    [HttpPut("politicalparty")]
    public async Task<IActionResult> UpdatePoliticalParty([FromForm] UpdatePoliticalPartyRequest request)
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
            await _repos.UpdatePoliticalPartyAsync(request);
            return Ok(new { message = "Political party information updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the political party information.", details = ex.Message });
        }
    }


    /// <summary>
    /// Updates the military information for an employee.
    /// </summary>
    /// <param name="militaryInfo">The updated military information details.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>

    [HttpPut("militaryInfo")]
    public async Task<IActionResult> UpdateMilitaryInfo([FromForm] EmployeeMilitaryInfo militaryInfo)
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
            await _repos.UpdateMilitaryInfoAsync(militaryInfo);
            return Ok(new { message = "Military information updated successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
    }

    /// <summary>
    /// Update Employee's photo By Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newImage"></param>
    /// <returns></returns>

    [HttpPut("photo")]
    public async Task<IActionResult> UpdatePhotoUrl(int id, IFormFile newImage)
    {
        string newPhotoName = string.Empty;
        bool success = false;

        try
        {
            // Upload the new file
            newPhotoName = FileUploader.UploadFile(newImage);

            // Update the database and get the old photo URL
            var oldPhotoName = await _repos.UpdateEmployeePhotoAsync(id, newPhotoName);

            // Delete the old photo
            if (!string.IsNullOrEmpty(oldPhotoName))
            {
                FileEraser.DeleteImage(oldPhotoName);
            }
            success = true; // Mark as successful
            return Ok(new { Success = success, Message = "Photo updated successfully." });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "Database conflict occurred", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
        }
        finally
        {
            // Cleanup the uploaded file if the operation was not successful
            if (!success && !string.IsNullOrEmpty(newPhotoName))
            {
                FileEraser.DeleteImage(newPhotoName);
            }
        }
    }

    #endregion



    #region Delete

    /// <summary>
    /// Soft deletes an employee, marking them as inactive.
    /// </summary>
    /// <param name="id">The ID of the employee to be deleted.</param>
    /// <returns>A success message if the deletion is successful, or an error if a database or server issue occurs.</returns>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            // Call the repository method to perform the soft delete
            await _repos.DeleteEmployeeAsync(id);

            return Ok(new { message = "Employee has been set as inactive successfully." });
        }
        catch (SqlException ex)
        {
            // Handle SQL-specific errors
            return StatusCode(500, new { message = "Database error occurred.", details = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle general errors
            return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
        }
    }
    #endregion
}