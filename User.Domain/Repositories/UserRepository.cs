using User.Domain.Exceptions;
using User.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace User.Domain.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<JustUser> AddUserAsync(JustUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<JustUser> DeleteUserAsync(JustUser user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<JustUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
    public async Task<JustUser> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<JustUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<List<JustUser>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    public async Task<JustUser> UpdateUserAsync(JustUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
