using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Entities;

namespace User.Domain.Repositories;

public interface IRolesRepository
{
    Task<Role> GetRoleByName(string roleName);
}
