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
            var productForCreate = _mapper.Map<mango.product.domain.Models.Product>(product);
            var productCreated = await _productsRepository.Create(productForCreate);
            return _mapper.Map<Product>(productCreated);
        }
        public async Task<bool> Update(Product product)
        {
            var productForUpdate = _mapper.Map<DomainModels.Product>(product);
             _productsRepository.Update(productForUpdate);
            return await _productsRepository.SaveChanges();

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

    }
}
