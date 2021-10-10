using mango.product.data.Context;
using mango.product.data.Infrastructure.ChangeCache;
using mango.product.domain.Builders;
using mango.product.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using DataModels = mango.product.data.Models;
using DomainModels = mango.product.domain.Models;

namespace mango.product.data.Repositories
{
    internal class ItemChanged<T>
    {

        public ItemChanged(Guid id, T item)
        {
            Id = id;
            Item = item;
        }
        public Guid Id { get; internal set; }
        public T Item { get; internal set; }

    }


    public class ProductRepository : IProductsRepository
    {

        private readonly ItemsChanged<DataModels.Product> updatedProducts = new ItemsChanged<DataModels.Product>();

        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public DomainModels.Product GetScope(Guid key)
        {
            var entity = updatedProducts.GetScope(key);
            if(entity!=null)
                return ToModel(entity);
            return null;

        }


        public Guid Create(DomainModels.Product product)
        {

            DataModels.Product entity = ToData(product);

            Guid key = updatedProducts.SetScope(entity);

            _dbContext.Products.Add(entity);

            return key;

        }

        public Guid Update(DomainModels.Product product)
        {

            DataModels.Product entity = ToData(product);

            Guid key = updatedProducts.SetScope(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            return key;

        }

        public bool Delete(int id)
        {
            var entity = _dbContext.Products.FirstOrDefault(p => p.ProductId == id);

            if (entity == null)
                return false;

            _dbContext.Products.Remove(entity);

            return true;

        }

        public async Task<DomainModels.Product> GetProductById(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return ToModel(entity);

        }

        public async Task<IEnumerable<DomainModels.Product>> GetProductsByCategory(string category)
        {
            var entities = await _dbContext.Products.Where(p => p.CategoryName == category).ToListAsync();

            var products = entities.Select(entity =>
            {
                return ToModel(entity);

            });


            return products;
        }

        public async Task<IEnumerable<DomainModels.Product>> GetProductsByName(string name)
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

        public async Task<IEnumerable<DomainModels.Product>> GetProducts()
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



        private void Detach<T>(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
        private DomainModels.Product ToModel(DataModels.Product product)
        {
            var productBuilder = new ProductBuilder();

            productBuilder.SetName(product.Name);
            productBuilder.SetPrice(product.Price);
            productBuilder.SetProductId(product.ProductId);
            productBuilder.SetCategoryName(product.CategoryName);
            productBuilder.SetImageUrl(product.ImageUrl);
            productBuilder.SetDescription(product.Description);
            productBuilder.SetRowVersion(product.RowVersion);

            return productBuilder.Build();

        }

        private DataModels.Product ToData(DomainModels.Product product)
        {
            DataModels.Product entity = new DataModels.Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.CategoryName,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                RowVersion = product.RowVersion,
            };

            return entity;
        }

    }
}


