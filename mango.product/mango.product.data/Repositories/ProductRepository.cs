using mango.product.data.Context;
using mango.product.domain.Interfaces;

using DomainModel = mango.product.domain.Models;
using DataModel = mango.product.data.Models;

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
            await _dbContext.Products.AddAsync(product);
            return product;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            _dbContext.Products.Remove(entity);
            return true;
        }

        public async Task<DomainModel.Product> Get(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            var productBuilder = new DomainModel.ProductBuilder();

            productBuilder.SetName(entity.Name);
            productBuilder.SetPrice(entity.Price);
            productBuilder.SetProductId(entity.ProductId);
            productBuilder.SetCategoryName(entity.CategoryName);
            productBuilder.SetImageUrl(entity.ImageUrl);
            productBuilder.SetDescription(entity.Description);


            DomainModel.Product product = productBuilder.Build();


            return product;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductByCategory(string category)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.CategoryName == category);
            return entity;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductByName(string name)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
            return entity;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProducts()
        {
            var entities = await _dbContext.Products.ToListAsync();
            return entities;
        }

        public async Task<bool> Update(DomainModel.Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            return true;
        }
        public async Task<bool> SaveChanges()
        {
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }


}


