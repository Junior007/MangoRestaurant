using mango.product.DAL.Models;
using MediatR;


namespace mango.product.DAL.Requests.QueryModels
{
    internal class GetProductsByName : IRequest<List<Product>>
    {
        public string Name { get; set; }
    }
}
