using mango.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Services.IServices
{
    public interface ICartService
    {
        Task<ResponseDto<T>> GetCartByUserIdAsnyc<T>(string userId, string token = null);
        Task<ResponseDto<T>> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<ResponseDto<T>> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<ResponseDto<T>> RemoveFromCartAsync<T>(int cartId, string token = null);
        Task<ResponseDto<T>> ApplyCoupon<T>(CartDto cartDto, string token = null);
        Task<ResponseDto<T>> RemoveCoupon<T>(string userId, string token = null);

        Task<ResponseDto<T>> Checkout<T>(CartHeaderDto cartHeader, string token = null);

    }
}
