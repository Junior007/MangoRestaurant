﻿using mango.product.DAL.Models;
using MediatR;

namespace mango.product.DAL.Request.QueryModels
{
    public class GetProducts : IRequest<List<Product>>
    {

    }
}
