using AutoMapper;
using User.Domain.Models.Response;

namespace User.Application.Services;

public class UserServices : IUserService
{
    private readonly IMapper _mapper;

    public UserServices(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<UserResponseDTO> GetUserAsync(int userId)
    {
        //var user = _db.Users.Find(userId);
        var user = new Domain.Models.Entities.JustUser()
        {
            Username = "aaa",
            PasswordHash = "asas",
            IsActive = true,
            Id = userId,
            Email = "User@email.commmm"
        };
        if (user == null)
            throw new Exception("Record not found");

        // Simulating async operation
        return _mapper.Map<UserResponseDTO>(user);
    }
}