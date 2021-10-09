using AutoMapper;
using mango.product.application.Interfaces;
using mango.product.application.Models;
using mango.product.domain.Interfaces;
using DomainModels = mango.product.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mango.product.domain.Builders;

namespace mango.product.application.Services
{
    public class ProductService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Product> Create(Product product)
        {
            var productForCreate = ToModel(product);
            Guid key = _productsRepository.Create(productForCreate);

            await _productsRepository.SaveChanges();

            DomainModels.Product productCreated = _productsRepository.GetScope(key);

            return _mapper.Map<Product>(productCreated);
        }
        public async Task<Product> Update(Product product)
        {
            var productForUpdate = ToModel(product);
            Guid key = _productsRepository.Update(productForUpdate);

            await _productsRepository.SaveChanges();

            DomainModels.Product productUpdated = _productsRepository.GetScope(key);

            return _mapper.Map<Product>(productUpdated);

        }
        public async Task<bool> Delete(int id)
        {
            _productsRepository.Delete(id);
            return await _productsRepository.SaveChanges();
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _productsRepository.GetProducts();

            return _mapper.Map<IEnumerable<Product>>(products);

        }

        public async Task<Product> Get(int id)
        {
            var products = await _productsRepository.GetProductById(id);

            return _mapper.Map<Product>(products);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var products = await _productsRepository.GetProductsByCategory(category);

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var products = await _productsRepository.GetProductsByName(name);

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        private DomainModels.Product ToModel(Product product)
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
    }
}
