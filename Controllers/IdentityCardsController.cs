using HumanResourcesWebApi.Models.Requests.IdentityCards;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityCardsController(IIdentityCardsRepository repos) : ControllerBase
    {
        private readonly IIdentityCardsRepository _repos = repos;

        /// <summary>
        /// Retrieves the identity card information for a specific employee by their ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee whose identity card information is being retrieved.</param>
        /// <returns>The identity card information for the specified employee, or a 404 if not found.</returns>
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetIdentityCard(int employeeId) 
        {
            try
            {
                var identityCard = await _repos.GetIdentityCardAsync(employeeId);
                if (identityCard == null)
                {
                    return NotFound(new { message = "No identity card found for the specified employee." });
                }
                return Ok(identityCard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new identity card for an employee.
        /// </summary>
        /// <param name="request">The details of the identity card to be added.</param>
        /// <returns>A success message if the identity card is added, or an error if validation fails or a server error occurs.</returns>
        [HttpPost]
        public async Task<IActionResult> AddIdentityCard([FromForm] AddIdentityCardRequest request) 
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
                await _repos.AddIdentityCardAsync(request);
                return Ok(new { message = "Identity card added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the identity card.", details = ex.Message });
            }
        }

        /// <summary>
        /// Updates the identity card information for an employee.
        /// </summary>
        /// <param name="request">The updated identity card details.</param>
        /// <returns>A success message if the update is successful, or an error if validation fails or a server error occurs.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateIdentityCard([FromForm] UpdateIdentityCardRequest request)
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
                await _repos.UpdateIdentityCardAsync(request);
                return Ok(new { message = "Identity card updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the identity card.", details = ex.Message });
            }
        }
    }
}
