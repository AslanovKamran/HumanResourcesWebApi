using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Models.Requests.Awards;
using HumanResourcesWebApi.Models.Requests.Rights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        private readonly IRightsRepository _repos;
        public RightsController(IRightsRepository repos) => _repos = repos;

        #region Add
        /// <summary>
        /// Add a new Right into the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> AddRight([FromForm] AddRightRequest request)
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
                await _repos.AddRightAsync(request);
                return Ok(new { message = "Right added successfully." });
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
        /// Get an individual Right
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRightById(int id)
        {
            try
            {
                var right = await _repos.GetRightByIdAsync(id);
                if (right == null)
                {
                    return NotFound(new { message = "No right found for the specified id." });
                }
                return Ok(right);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }


        /// <summary>
        /// Get a list of Rights
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<IActionResult> GetRights()
        {
            try
            {
                var rights = await _repos.GetRightsListAsync();
                if (rights == null || rights.Count <= 0)
                {
                    return NotFound(new { message = "No rights found " });
                }
                return Ok(rights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }



        #endregion

        #region Update

        /// <summary>
        /// Update a Right by its Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         
        [HttpPut]
        public async Task<IActionResult> UpdateRight([FromForm] UpdateRightRequest request)
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
                await _repos.UpdateRightByIdAsync(request);
                return Ok(new { message = "Right updated successfully." });
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
}
