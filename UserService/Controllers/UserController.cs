using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.Application.Services;
using User.Domain.Exceptions;
using User.Domain.Models.Entities;
using User.Domain.Models.Response;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet]
    public async Task<ActionResult> GetUserData()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        try
        {
            var userDto = await _userService.GetUserAsync(userId);
            return Ok(userDto);
        }
        catch
        {
            return NotFound();
        }
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet("GetClientByID")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet("GetClientByEmail")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        try
        {
            var result = await _userService.GetUserByEmailAsync(email);
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet("GetClientByUsername")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        try
        {
            var result = await _userService.GetUserByUsernameAsync(username);
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsers(int id, [FromBody] UserResponseDTO userResponseDTO)
    {
        if (id != userResponseDTO.Id)
            return BadRequest("ID in route does not match ID in body.");

        try
        {
            var result = await _userService.UpdateUserAsync(userResponseDTO); // Service expects DTO
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message); // more correct than BadRequest here
        }
    }

    [Authorize(Policy = "EmployeeOnly")]
    [HttpDelete("DeleteUsers")]
    public async Task<IActionResult> DeleteUsers(int id)
    {
        try
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }
        catch (UserDoesntExistsExeption ex)
        {
            return NotFound(ex.Message);
        }
    }
}