using Cart.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application;

public interface IEShopConnectService
{
    Task<ProductGetDTO> GetProductAsync(int id);
}
