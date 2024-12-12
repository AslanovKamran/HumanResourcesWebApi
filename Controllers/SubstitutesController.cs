using HumanResourcesWebApi.Models.Requests.Substitutes;
using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubstitutesController : ControllerBase
    {
        private readonly ISubstitutesRepository _repos;
        public SubstitutesController(ISubstitutesRepository repos) => _repos = repos;

        #region Get


        /// <summary>
        /// Get Subs by Employee Id and Vacation Id
        /// </summary>
        /// <param name="whomId"></param>
        /// <param name="vacationId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("vacation")]
        public async Task<IActionResult> GetByVacation(int whomId, int vacationId)
        {
            try
            {
                var result = await _repos.GetSubstituteByVacationsAsync(whomId, vacationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get Subs by Employee Id and Bulletin Id
        /// </summary>
        /// <param name="whomId"></param>
        /// <param name="bulletinId"></param>
        /// <returns></returns>


        [HttpGet]
        [Route("bulletin")]
        public async Task<IActionResult> GetByBulletin(int whomId, int bulletinId)
        {
            try
            {
                var result = await _repos.GetSubstituteByBulletinAsync(whomId, bulletinId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Add
        /// <summary>
        /// Add new Sub entry 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]  AddSubstituteRequest request)
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
                await _repos.AddSubstituteAsync(request);
                return Ok(new { message = "Substitute added successfully." });
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
        /// Update Sub entry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSubstituteRequest request) 
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
                await _repos.UpdateSubstituteAsync(request);
                return Ok(new { message = "Substitute updatetd successfully." });
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
        /// Delete an individual Sub entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repos.DeleteSubstituteByIdAsync(id);
                return Ok(new { message = "Substitute deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the substitute.", details = ex.Message });
            }
        }

        #endregion


    }
}
