using EShopDomain.Models;

namespace EShop.Domain.Repositories
{
    public interface IRepository
    {
        #region Product
        Task<Product> GetProductAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product user);
        Task<List<Product>> GetAllProductAsync();
        Task<List<Product>> GetAllProductsByNameAsync(string input);
        #endregion


        #region Category
        Task<Category> GetCategoryAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoryAsync();
        #endregion
    }
}
