using mango.product.DAL.Models;
using MediatR;

namespace mango.product.DAL.Request.QueryModels
{
    class GetProducts : IRequest<List<Product>>
    {

    }
}
