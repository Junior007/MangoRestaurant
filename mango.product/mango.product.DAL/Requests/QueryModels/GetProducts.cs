using mango.product.DAL.Models;
using MediatR;

namespace mango.product.DAL.Requests.QueryModels
{
    public class GetProducts : IRequest<List<Product>>
    {

    }
}
