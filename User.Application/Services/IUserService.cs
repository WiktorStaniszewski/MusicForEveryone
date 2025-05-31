using User.Domain.Models.Response;
using User.Domain.Models.Entities;

namespace User.Application.Services;

public interface IUserService
{
    Task<UserResponseDTO> GetUserAsync(int userId);
}
