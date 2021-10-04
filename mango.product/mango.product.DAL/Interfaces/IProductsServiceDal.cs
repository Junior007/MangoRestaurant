using mango.product.DAL.Models;

namespace mango.product.DAL.Interfaces
{
    public interface IProductsServiceDal
    {
        Task<List<Product>> Get();
        Task<Product> Get(int productId);
    }
}