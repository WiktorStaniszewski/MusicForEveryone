using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models.Entities;

namespace User.Application.Services;

public interface IRegisterService
{
    Task<JustUser> RegisterClient(string username, string email, string password);
}
