using mango.product.DAL.Models;
using MediatR;

namespace mango.product.DAL.Requests.QueryModels
{
    public class GetProductsByCategory:IRequest<List<Product>>
    {
        public string CategoryName { get; set; }
    }
}
