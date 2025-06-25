using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Entities;

namespace User.Domain.Repositories;
public class RolesRepository : IRolesRepository
{
    private readonly DataContext _context;

    public RolesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Role> GetRoleByName(string roleName)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}
