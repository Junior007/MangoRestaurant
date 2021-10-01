using mango.product.DAL.Models;
using MediatR;

namespace mango.product.DAL.Request.QueryModels
{
    class GetProduct : IRequest<Product>
    {
        public int ProductId { get; set; }
    }
}
