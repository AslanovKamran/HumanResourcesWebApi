using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificatesRepository _repos;

        public CertificatesController(ICertificatesRepository repos) => _repos = repos;

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

    }
}
