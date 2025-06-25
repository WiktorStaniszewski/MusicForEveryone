using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Models.Requests;

public class PasswordChangeRequest
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
}
