using mango.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto<T>> GetCoupon<T>(string couponCode, string token = null);

    }
}
