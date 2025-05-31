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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = _loginService.Login(request.Username, request.Password);
            return Ok(new { token });
        }
        catch (InvalidCredentialsException)
        {
            return Unauthorized(); 
        }
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        try
        {
            var token = _registerService.RegisterClient(request.Username, request.Email, request.Password);
            return Ok(new { token });
        }
        catch (UserExistsExeption)
        {
            return Conflict("User already exists in the system.");
        }
        catch (InvalidCredentialsException)
        {
            return BadRequest("Username and password cannot be empty.");
        }
    }
    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}