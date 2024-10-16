using Dapper;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.Requests.IdentityCards;
using HumanResourcesWebApi.Repository.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityCardsController : ControllerBase
    {
        private readonly IIdentityCardsRepository _repos;
        public IdentityCardsController(IIdentityCardsRepository repos) => _repos = repos;

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
