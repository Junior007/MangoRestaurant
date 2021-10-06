using mango.product.DAL.Models;

namespace mango.product.DAL.Interfaces
{
    public interface IProductsServiceDal
    {
        Task<List<Product>> Get();
        Task<Product> Get(int productId);
        Task<List<Product>> GetProductsByName(string name);
        Task<List<Product>> GetProductsByCategory(string category);
    }
}