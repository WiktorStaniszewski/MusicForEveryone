using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Models;

public class Order : BaseModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
}
