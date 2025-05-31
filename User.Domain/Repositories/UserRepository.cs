using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Entities;

namespace User.Domain.Repositories;

public class UserRepository : IUserRepository
{
    // Implement the methods defined in IUserRepository interface
    public Task<JustUser> AddUserAsync(JustUser user)
    {
        // Implementation for adding a user
        throw new NotImplementedException();
    }
    public Task<JustUser> DeleteUserAsync(JustUser user)
    {
        // Implementation for deleting a user
        throw new NotImplementedException();
    }
    public Task<JustUser> GetUserByUsernameAsync(string username)
    {
        // Implementation for getting a user by username
        throw new NotImplementedException();
    }
    public Task<JustUser> GetUserByEmailAsync(string email)
    {
        // Implementation for getting a user by email
        throw new NotImplementedException();
    }
    public Task UpdateUserAsync(JustUser user)
    {
        // Implementation for updating a user
        throw new NotImplementedException();
    }
}
