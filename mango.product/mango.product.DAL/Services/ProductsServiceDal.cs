using mango.product.DAL.Interfaces;
using mango.product.DAL.Models;
using mango.product.DAL.Request.QueryModels;
using MediatR;

namespace mango.product.DAL.Services
{

    public class ProductsServiceDal : IProductsServiceDal
    {
        private readonly IMediator _mediator;

        public ProductsServiceDal(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<List<Product>> Get()
        {
            var requestModel = new GetProducts { };
            return await _mediator.Send(requestModel);

        }
        public async Task<Product> Get(int productId)
        {
            var requestModel = new GetProduct { ProductId = productId };
            return await _mediator.Send(requestModel);
        }
    }


}
