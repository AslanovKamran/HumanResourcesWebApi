using HumanResourcesWebApi.Models.Requests.Certificates;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CertificatesController(ICertificatesRepository repos) : ControllerBase
{
    private readonly ICertificatesRepository _repos = repos;

    #region Get

    /// <summary>
    /// Retrieves a list of certificates for a specific employee by their ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee whose certificates are being retrieved.</param>
    /// <returns>A list of certificates associated with the specified employee, or an error if a conflict or server error occurs.</returns>
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeesCertificates(int employeeId) 
    {
        try
        {
            var result = await _repos.GetEmployeesCertificates(employeeId);
            return Ok(result);
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

    #region Add

    /// <summary>
    /// Adds a new certificate for an employee.
    /// </summary>
    /// <param name="request">The details of the certificate to be added.</param>
    /// <returns>A success message if the certificate is added, or a validation or conflict error if an issue occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> AddCertificate([FromForm] AddCertificateRequest request) 
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
            await _repos.AddCertificateAsync(request);
            return Ok(new { message = "Certificate added successfully", data = request});
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
    /// Updates an existing certificate based on the provided details.
    /// </summary>
    /// <param name="request">The updated certificate details.</param>
    /// <returns>A success message if the update is successful, or an error if validation fails or a conflict occurs.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateCertificate([FromForm] UpdateCertificateRequest request)
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
            await _repos.UpdateCertificateAsync(request);
            return Ok(new { message = "Certificate updated successfully", data = request });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = "An error occurred while updating the certificate", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred", details = ex.Message });
        }
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes a certificate by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the certificate to delete.</param>
    /// <returns>A success message if the deletion is successful, or an error message if a conflict or server error occurs.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        try
        {
            await _repos.DeleteCertificateAsync(id);
            return Ok(new { message = "Certificate deleted successfully" });
        }
        catch (SqlException ex)
        {
            return Conflict(new { message = "An error occurred while deleting the certificate", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred", details = ex.Message });
        }
    }

    #endregion
}
