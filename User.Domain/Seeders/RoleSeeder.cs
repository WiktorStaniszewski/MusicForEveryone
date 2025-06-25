using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Entities;
using User.Domain.Repositories;
using User.Domain.Security;


namespace User.Domain.Seeders;

public class RoleSeeder : IRoleSeeder
{
    private readonly DataContext _context;
    private readonly IPasswordHash _passwordHash;

    public RoleSeeder(DataContext context, IPasswordHash passwordHash)
    {
        _context = context;
        _passwordHash = passwordHash;
    }
    public async Task SeedRole()
    {
        if (!_context.Roles.Any())
        {
            var roles = new List<Role>
        {
            new Role { Name = "Admin" },
            new Role { Name = "Client" },
            new Role { Name = "Employee" }
        };
            _context.Roles.AddRange(roles);
            await _context.SaveChangesAsync();
        }
        if (!_context.Users.Any())
        {
            var adminRole = _context.Roles.First(r => r.Name == "Admin");
            var employeeRole = _context.Roles.First(r => r.Name == "Employee");
            var clientRole = _context.Roles.First(r => r.Name == "Client");

            var users = new List<JustUser>
        {
            new JustUser
            {
                Username = "admin",
                Email = "admin@googmail.com",
                PasswordHash = _passwordHash.HashPassword("admin"),
                Roles = new List<Role> { adminRole, employeeRole, clientRole }
            },
            new JustUser
            {
                Username = "employee",
                Email = "krzyziek@gmail.onet.pl",
                PasswordHash = _passwordHash.HashPassword("employee"),
                Roles = new List<Role> { employeeRole }
            },
            new JustUser
            {
                Username = "employee1",
                Email = "monika@wp.usa.com",
                PasswordHash = _passwordHash.HashPassword("employee"),
                Roles = new List<Role> { employeeRole }
            },
            new JustUser
            {
                Username = "employee2",
                Email = "huraganKatrina@example.com",
                PasswordHash = _passwordHash.HashPassword("employee"),
                Roles = new List<Role> { employeeRole }
            }
        };

            _context.Users.AddRange(users);

            await _context.SaveChangesAsync();
        }
    }
}
