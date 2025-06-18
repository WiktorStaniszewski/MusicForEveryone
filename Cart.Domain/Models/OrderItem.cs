using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Models;

[PrimaryKey(nameof(Id), nameof(OrderId))]
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
