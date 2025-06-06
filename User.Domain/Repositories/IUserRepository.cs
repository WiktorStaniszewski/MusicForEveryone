﻿using User.Domain.Models.Entities;

namespace User.Domain.Repositories;

public interface IUserRepository
{
    // Define methods for user repository operations, e.g.:
    Task<JustUser> AddUserAsync(JustUser user);
    Task<JustUser> DeleteUserAsync(JustUser user);
    Task<JustUser> GetUserByUsernameAsync(string username);
    Task<JustUser> GetUserByEmailAsync(string email);
    Task<JustUser> GetUserByIdAsync(int id);
    Task<List<JustUser>> GetAllUsersAsync();
    Task<JustUser> UpdateUserAsync(JustUser user);
}
