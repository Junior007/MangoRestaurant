using mango.web.Models;
using mango.web.Services.IServices;
using mango.web.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace mango.web.Services
{
    public class CartService : BaseService,ICartService
    {

        private readonly IServiceUrls _serviceUrls;
        public CartService(IHttpClientFactory clientFactory, IServiceUrls serviceUrls, ILogger<CartService> logger) : base(clientFactory, logger)
        {

            _serviceUrls=serviceUrls;
        }
        public async Task<ResponseDto<T>> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> ApplyCoupon<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> Checkout<T>(CartHeaderDto cartHeader, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartHeader,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/checkout",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> GetCartByUserIdAsnyc<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId,
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> RemoveCoupon<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = userId,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartId,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<ResponseDto<T>> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Post,
                Data = cartDto,
                Url = _serviceUrls.ShoppingCartAPIBase + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
