using mango.web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<ResponseDto<List<ProductDto>>> GetAllProductsAsync(string token);
        Task<ResponseDto<ProductDto>> GetProductByIdAsync(int id, string token);
        Task<ResponseDto<ProductDto>> CreateProductAsync(ProductDto productDto, string token);
        Task<ResponseDto<ProductDto>> UpdateProductAsync(ProductDto productDto, string token);
        Task<ResponseDto<bool>> DeleteProductAsync(ProductDto productDto, string token);
    }
}
