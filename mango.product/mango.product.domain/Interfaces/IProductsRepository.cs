using mango.product.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<Product> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);
        Task<bool> SaveChanges();


    }
}
