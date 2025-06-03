using EShop.Domain.Repositories;
using EShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly IRepository _repository;

    public CategoryService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _repository.GetAllCategoryAsync();
    }

    public async Task<Category> GetAsync(int id)
    {
        return await _repository.GetCategoryAsync(id);
    }
    public async Task<Category> AddAsync(Category category)
    {
        return await _repository.AddCategoryAsync(category);
    }
}
