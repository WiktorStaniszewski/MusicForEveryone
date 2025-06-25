using User.Domain.Exceptions;
using User.Domain.Models.Entities;
using User.Domain.Security;
using User.Domain.Repositories;
using User.Application.Producer;

namespace User.Application.Services;

public class RegisterService : IRegisterService
{
    protected IJwtTokenService _jwtTokenService;
    protected IPasswordHash _passwordHash;
    protected IUserRepository _userRepository;
    protected IKafkaProducer _kafkaProducer;

    public RegisterService(IJwtTokenService jwtTokenService, IPasswordHash passwordHash, IUserRepository userRepository, IKafkaProducer kafkaProducer)
    {
        _jwtTokenService = jwtTokenService;
        _passwordHash = passwordHash;
        _userRepository = userRepository;
        _kafkaProducer = kafkaProducer;
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
        // await _kafkaProducer.SendAsync("user-registration-topic", newUser.Email);
        return newUser;

    }
}


