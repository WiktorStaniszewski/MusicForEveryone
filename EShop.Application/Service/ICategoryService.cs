using EShop.Domain.Repositories;
using EShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    Task<Category> GetAsync(int id);
    Task<Category> AddAsync(Category category);
}
