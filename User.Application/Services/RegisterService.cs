using User.Domain.Exceptions;
using User.Domain.Models.Entities;
using User.Domain.Security;
using User.Domain.Repositories;

namespace User.Application.Services;

public class RegisterService : IRegisterService
{
    protected IJwtTokenService _jwtTokenService;
    protected IPasswordHash _passwordHash;
    protected IUserRepository _userRepository;

    public RegisterService(IJwtTokenService jwtTokenService, IPasswordHash passwordHash, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService;
        _passwordHash = passwordHash;
        _userRepository = userRepository;
    }

    public async Task<JustUser> RegisterClient(string username, string email, string password)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(username);
        var existingByEmail = await _userRepository.GetUserByEmailAsync(email);
        
        if (existingByEmail != null)
        {
            throw new UserExistsExeption("Email already in use. Please login.");
        }
        if (existingUser != null)
        {
            throw new UserExistsExeption();
        }
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidCredentialsException();
        }

        var hashedPassword = _passwordHash.HashPassword(password);
        
        var newUser = new JustUser
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword,
            CreatedAt = default,
            Roles = new List<Role> { new Role { Name = "Client" } },
            IsActive = true
        };

        await _userRepository.AddUserAsync(newUser); 
        return newUser;

    }
}


