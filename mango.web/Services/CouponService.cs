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
    public class CouponService : BaseService,ICouponService
    {
        private readonly IServiceUrls _serviceUrls;

        public CouponService(IHttpClientFactory clientFactory, IServiceUrls serviceUrls, ILogger<CartService> logger) : base(clientFactory, logger)
        {
            _serviceUrls = serviceUrls;
        }
        public async Task<ResponseDto<T>> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = HttpMethod.Get,
                Url = _serviceUrls.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
