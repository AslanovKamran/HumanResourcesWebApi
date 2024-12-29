using HumanResourcesWebApi.Common.AuthenticationService;
using HumanResourcesWebApi.Models.Requests.Users;
using Microsoft.AspNetCore.Authorization;
using HumanResourcesWebApi.Abstract;
using HumanResourcesWebApi.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HumanResourcesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repos;
    private readonly ITokenGenerator _tokenGenerator;

    public UsersController(IUserRepository repos, ITokenGenerator tokenGenerator)
    {
        _repos = repos;
        _tokenGenerator = tokenGenerator;
    }

    #region Get

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var result = await _repos.GetAllUsersAsync();

            if (result == null || result.Count <= 0)
            {
                return NotFound(new { message = $"No users found in the database." });
            }

            // Return the result as a successful response
            return Ok(result);
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


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var result = await _repos.GetUserByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { message = $"No user with {id} Id found in the database." });
            }

            // Return the result as a successful response
            return Ok(result);
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

    #endregion

    #region Add - Sign up a new User

    [HttpPost]
    [Route("signUp")]
    public async Task<IActionResult> SignUp([FromForm] SignUpUserRequest request)
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
            var response = await _repos.SignUpUserAsync(request);

            return Ok(new { message = $"User Created Successfully with id {response}" });
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

    #region Login

    [HttpPost]
    [Route("logIn")]
    public async Task<IActionResult> LogInUser([FromForm] LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        var user = await _repos.GetUserByIdUserNameAsync(request.UserName);

        if (user == null)
            return Unauthorized("Invalid login credentials.");

        var hashedInputPassword = AuthService.HashPassword(request.Password, user.Salt);

        if (hashedInputPassword != user.Password)
            return Unauthorized("Invalid login credentials.");

        var accessToken = _tokenGenerator.GenerateAccessToken(user);

        var response = new
        {
            AccessToken = accessToken,
            User = user
        };
        return Ok(response);
    }

    #endregion


    #region Change Password

    [HttpPost]
    [Route("changePassword")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordRequest request)
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
            await _repos.ChangeUserPasswordByIdAsync(request);
            return Ok(new
            {
                Message = $"Password of the user has been changed successfully",
                Success = true
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

    #endregion
}
