using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Exceptions;
using User.Domain.Repositories;
using User.Domain.Security;

namespace User.Application.Services;

public class LoginService : ILoginService
{
    protected IJwtTokenService _jwtTokenService;
    protected IUserRepository _userRepository;
    protected IPasswordHash _passwordHash;

    public LoginService(IJwtTokenService jwtTokenService, IUserRepository userRepository, IPasswordHash passwordHash)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _passwordHash = passwordHash;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        /*
        if (username == "admin" && password == "password")
        {
            var adminRoles = new List<string> { "Client", "Employee", "Administrator" };
            var adminToken = _jwtTokenService.GenerateToken(123, adminRoles);
            return adminToken;
        }
        */        

        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new UserDoesntExistsExeption("User with this username does not exist.");

        }
        var isPasswordValid = _passwordHash.VerifyPassword(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new InvalidCredentialsException("Invalid password.");
        }
        user.LastLoginAt = DateTime.UtcNow;
        await _userRepository.UpdateUserAsync(user);

        var roles = user.Roles.Select(r => r.Name).ToList();
        var token = _jwtTokenService.GenerateToken(user.Id, roles);
        return token;
        
    }
}