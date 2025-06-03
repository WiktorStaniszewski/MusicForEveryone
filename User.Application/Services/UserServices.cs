using AutoMapper;
using User.Domain.Models.Response;
using User.Domain.Repositories;
using User.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using User.Domain.Models.Entities;

namespace User.Application.Services;

public class UserServices : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserServices(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserResponseDTO> GetUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new UserDoesntExistsExeption("User with this ID does not exist.");
        }
        return _mapper.Map<UserResponseDTO>(user);
    }
    public async Task<List<UserResponseDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        if (users == null || !users.Any())
        {
            throw new UserDoesntExistsExeption("No users found.");
        }
        return users.Select(u => _mapper.Map<UserResponseDTO>(u)).ToList();
    }
    public async Task<UserResponseDTO> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new UserDoesntExistsExeption("User with this username does not exist.");
        }
        return _mapper.Map<UserResponseDTO>(user);
    }
    public async Task<UserResponseDTO> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new UserDoesntExistsExeption("User with this email does not exist.");
        }
        return _mapper.Map<UserResponseDTO>(user);
    }
    public async Task<UserResponseDTO> UpdateUserAsync(UserResponseDTO userDto)
    {
        var user = _mapper.Map<JustUser>(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(user);
        return _mapper.Map<UserResponseDTO>(updatedUser);
    }
    public async Task<UserResponseDTO> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new UserDoesntExistsExeption();
        }
        var deletedUser = await _userRepository.DeleteUserAsync(user);
        return _mapper.Map<UserResponseDTO>(deletedUser);
    }
}