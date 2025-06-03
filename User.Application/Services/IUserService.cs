using User.Domain.Models.Response;
using User.Domain.Models.Entities;

namespace User.Application.Services;

public interface IUserService
{
    Task<UserResponseDTO> GetUserAsync(int id);
    Task<List<UserResponseDTO>> GetAllUsersAsync();
    Task<UserResponseDTO> DeleteUserAsync(int id);
    Task<UserResponseDTO> UpdateUserAsync(UserResponseDTO userDto);
    Task<UserResponseDTO> GetUserByEmailAsync(string email);
    Task<UserResponseDTO> GetUserByUsernameAsync(string username);
}
