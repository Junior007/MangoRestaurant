using mango.product.data.Context;
using mango.product.domain.Interfaces;

using DomainModel = mango.product.domain.Models;
using DataModel = mango.product.data.Models;
using mango.product.domain.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mango.product.data.Repositories
{


    public class ProductRepository : IProductsRepository
    {
        ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public async Task<DomainModel.Product> Create(DomainModel.Product product)
        {
            throw new NotImplementedException();
            /*
            await _dbContext.Products.AddAsync(product);
            return product;
*/
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            _dbContext.Products.Remove(entity);
            return true;
        }

        public async Task<DomainModel.Product> GetProductById(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            var productBuilder = new ProductBuilder();

            productBuilder.SetName(entity.Name);
            productBuilder.SetPrice(entity.Price);
            productBuilder.SetProductId(entity.ProductId);
            productBuilder.SetCategoryName(entity.CategoryName);
            productBuilder.SetImageUrl(entity.ImageUrl);
            productBuilder.SetDescription(entity.Description);


            DomainModel.Product product = productBuilder.Build();


            return product;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
            /*
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.CategoryName == category);
            return entity;
            */
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductsByName(string name)
        {
            throw new NotImplementedException();
            /*
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
            return entity;
*/
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProducts()
        {
            throw new NotImplementedException();
            /*
          var entities = await _dbContext.Products.ToListAsync();
            return entities;
*/
        }

        public async Task<bool> Update(DomainModel.Product product)
        {
            throw new NotImplementedException();
            /*
          _dbContext.Entry(product).State = EntityState.Modified;
            return true;
*/
        }
        public async Task<bool> SaveChanges()
        {
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }


}


