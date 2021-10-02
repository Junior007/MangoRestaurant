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
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public async Task<DomainModel.Product> Create(DomainModel.Product product)
        {
            DataModel.Product entity = ToData(product);
            await _dbContext.Products.AddAsync(entity);
            return ToModel(entity);

        }

        public void Update(DomainModel.Product product)
        {
            DataModel.Product entity = ToData(product);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (entity == null)
                    return false;
                _dbContext.Products.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<DomainModel.Product> GetProductById(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return ToModel(entity);

        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductsByCategory(string category)
        {
            var entities = await _dbContext.Products.Where(p => p.CategoryName == category).ToListAsync();

            var products = entities.Select(entity =>
            {
                return ToModel(entity);

            });


            return products;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProductsByName(string name)
        {
            var entities = await _dbContext.Products.Where(p => p.Name == name).ToListAsync();

            var products = entities.Select(entities =>
            {
                var productBuilder = new ProductBuilder();

                productBuilder.SetName(entities.Name);
                productBuilder.SetPrice(entities.Price);
                productBuilder.SetProductId(entities.ProductId);
                productBuilder.SetCategoryName(entities.CategoryName);
                productBuilder.SetImageUrl(entities.ImageUrl);
                productBuilder.SetDescription(entities.Description);


                return productBuilder.Build();

            });


            return products;
        }

        public async Task<IEnumerable<DomainModel.Product>> GetProducts()
        {//TODO: usar DAL
            var entities = await _dbContext.Products.ToListAsync();
            var products = entities.Select(entity =>
            {
                return ToModel(entity);

            });

            return products;
        }


        public async Task<bool> SaveChanges()
        {
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private DomainModel.Product ToModel(DataModel.Product product)
        {
            var productBuilder = new ProductBuilder();

            productBuilder.SetName(product.Name);
            productBuilder.SetPrice(product.Price);
            productBuilder.SetProductId(product.ProductId);
            productBuilder.SetCategoryName(product.CategoryName);
            productBuilder.SetImageUrl(product.ImageUrl);
            productBuilder.SetDescription(product.Description);

            return productBuilder.Build();

        }

        private DataModel.Product ToData(DomainModel.Product product)
        {
            DataModel.Product entity = new DataModel.Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.CategoryName,
                ImageUrl = product.ImageUrl,
                Description = product.Description
            };

            return entity;
        }

    }
}


