using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Common.Mapper;
using HumanResourcesWebApi.Models.DTO;
using HumanResourcesWebApi.Models.Requests.Cities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(ICitiesRepository repos) : ControllerBase
    {
        private readonly ICitiesRepository _repos = repos;

        [HttpGet]
        public async Task<IActionResult> GetCities([FromQuery] int itemsPerPage = 50, [FromQuery] int currentPage = 1)
        {
            try
            {
                var (cities, pageInfo) = await _repos.GetCitiesAsync(itemsPerPage, currentPage);
                return Ok(new
                {
                    Cities = cities,
                    PageInfo = pageInfo
                });
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
        public async Task<IActionResult> AddCity([FromForm] AddCityRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState
                    .Where(v => v.Value!.Errors.Any())
                    .Select(v => new { Field = v.Key, Errors = v.Value!.Errors.Select(e => e.ErrorMessage) })
                });

            }

            try
            {
                await _repos.AddCityAsync(request);
                return Ok(new { message = "City added successfully." });
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
        public async Task<IActionResult> UpdateCity([FromForm] UpdateCityRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState
                    .Where(v => v.Value!.Errors.Any())
                    .Select(v => new { Field = v.Key, Errors = v.Value!.Errors.Select(e => e.ErrorMessage) })
                });

            }

            try
            {
                await _repos.UpdateCityAsync(request);
                return Ok(new { message = "City updated successfully." });
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

        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int id) 
        {
            try
            {
                await _repos.DeleteCityAsync(id);
                return Ok("City has been deleted successfully");
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

    }
}
