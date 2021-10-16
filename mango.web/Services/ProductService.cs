using mango.web.Models;
using mango.web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace mango.web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
               ApiType = HttpMethod.Post,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/v1/Product",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Delete,
                Data = productDto,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{productDto.ProductId}",
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProductAPIBase + "/api/v1/Product",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{id}",
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Put,
                Data = productDto,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{productDto.ProductId}",
                AccessToken = token
            });
        }
    }
}
