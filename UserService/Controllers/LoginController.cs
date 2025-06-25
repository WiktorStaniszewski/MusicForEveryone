using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Services;
using User.Domain.Exceptions;
using User.Domain.Models.Requests;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    protected ILoginService _loginService;
    protected IRegisterService _registerService;
    protected IUserService _userService;

    public LoginController(ILoginService loginService, IRegisterService registerService, IUserService userService)
    {
        _registerService = registerService;
        _loginService = loginService;
        _userService = userService;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _loginService.LoginAsync(request.Username, request.Password);
            return Ok(new { token });
        }
        catch (InvalidCredentialsException)
        {
            return Unauthorized(); 
        }
        catch (UserDoesntExistsExeption)
        {
            return Unauthorized();
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var token = await _registerService.RegisterClient(request.Username, request.Email, request.Password);
            return Ok(new { token });
        }
        catch (UserExistsExeption ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (InvalidCredentialsException)
        {
            return BadRequest("Username and password cannot be empty.");
        }
    }
}