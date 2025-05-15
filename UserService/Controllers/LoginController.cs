using Microsoft.AspNetCore.Mvc;
using User.Domain;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Here you would normally validate username and password against a database
        if (request.Username == "admin" && request.Password == "password")
        {
            // Login successful
            return Ok("Login successful!");
        }
        else
        {
            // Login failed
            return Unauthorized("Invalid username or password.");
        }
    }
}