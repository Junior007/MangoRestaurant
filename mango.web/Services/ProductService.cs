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
        //private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory, ILogger<CartService> logger) : base(clientFactory, logger)
        {
            //_clientFactory = clientFactory;
        }

        public async Task<ResponseDto<ProductDto>> CreateProductAsync(ProductDto productDto, string token)
        {
            return await this.SendAsync<ProductDto>(new ApiRequest()
            {
               ApiType = HttpMethod.Post,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/v1/Product",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<bool>> DeleteProductAsync(ProductDto productDto, string token)
        {
            return await this.SendAsync<bool>(new ApiRequest()
            {
                ApiType = HttpMethod.Delete,
                Data = productDto,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{productDto.ProductId}",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllProductsAsync(string token)
        {
            return await this.SendAsync<List<ProductDto>>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProductAPIBase + "/api/v1/Product",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<ProductDto>> GetProductByIdAsync(int id, string token)
        {
            return await this.SendAsync<ProductDto>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{id}",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<ProductDto>> UpdateProductAsync(ProductDto productDto, string token)
        {
            return await this.SendAsync<ProductDto>(new ApiRequest()
            {
                ApiType = HttpMethod.Put,
                Data = productDto,
                Url = SD.ProductAPIBase + $"/api/v1/Product/{productDto.ProductId}",
                AccessToken = token
            });
        }

    }
}
