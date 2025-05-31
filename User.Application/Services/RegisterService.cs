using User.Domain.Exceptions;
using User.Domain.Models.Entities;
using User.Domain.Security;

namespace User.Application.Services;

public class RegisterService : IRegisterService
{
    protected IJwtTokenService _jwtTokenService;
    protected IPasswordHash _passwordHash;

    public RegisterService(IJwtTokenService jwtTokenService, IPasswordHash passwordHash)
    {
        _jwtTokenService = jwtTokenService;
        _passwordHash = passwordHash;
    }

    public async Task<JustUser> RegisterClient(string username, string email, string password)
    {
        if (username == "exists") // Simulating an existing user for demonstration purposes
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

        // await _userRepository.AddUserAsync(newUser); <-- later, subsequent to creation of UserRepository
        return newUser;

    }
}


