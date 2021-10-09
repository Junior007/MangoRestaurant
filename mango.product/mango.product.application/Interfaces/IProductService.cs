using mango.product.application.Models;

namespace mango.product.application.Interfaces
{

    public interface IProductsService
    {
        Task<IEnumerable<Product>> Get();
        Task<Product> Get(int id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(int id);
    }

}
