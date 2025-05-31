using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Exceptions;

public class UserExistsExeption : Exception
{
    public UserExistsExeption() : base("User already exists in the system.") { }
}
