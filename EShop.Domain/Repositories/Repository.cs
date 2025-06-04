using EShopDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Domain.Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext dataContext)
        {
            _context = dataContext;
        }

        //product
        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByNameAsync(string input)
        {
            List<Product> datalist = await _context.Products.ToListAsync();
            List<Product> output = new List<Product>();
            foreach (var element in datalist) 
            {
                if (element.Name.ToLower().StartsWith(input.ToLower()))
                {
                    output.Add(element);
                }
            }
            return output;
            //throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        //category
        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                throw new InvalidOperationException($"Category with Id {id} not found.");
            }
            return category;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
