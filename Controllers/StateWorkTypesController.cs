using HumanResourcesWebApi.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateWorkTypesController : ControllerBase
    {
        private readonly IStateWorkTypesRepository _repos;

        public StateWorkTypesController(IStateWorkTypesRepository repos) => _repos = repos;

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
            var result = await _repos.GetStateWorkTypesAsync();
            return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
                var result = await _repos.GetStateWorkTypeByIdAsync(id);
                return result ==null ? NotFound($"No work type with Id = {id}") : Ok(result);

        }

    }

}
