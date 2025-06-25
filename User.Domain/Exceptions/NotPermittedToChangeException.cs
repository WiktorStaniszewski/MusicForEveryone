using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Exceptions;

public class NotPermittedToChangeException : Exception
{
    public NotPermittedToChangeException(string message) : base(message) { }
    public NotPermittedToChangeException() : base("Input password does not match your password.") { }
}
