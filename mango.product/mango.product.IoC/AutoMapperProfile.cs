using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.product.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            //Application <--> Domain
            CreateMap<mango.product.domain.Models.Product, mango.product.application.Models.Product>().ReverseMap();


            //IN
            //CreateMap<ordering.domain.models.PaymentMethod, ordering.application.models.PaymentMethod>();

            // IN
            //CreateMap<ordering.application.models.Order, ordering.domain.models.Order>();
            //CreateMap<ordering.application.models.PaymentMethod, ordering.domain.models.PaymentMethod>();


            //Event Bus
            /*CreateMap<infra.eventbus.events.BasketCartCheckoutEvent, ordering.application.models.Order>().ReverseMap(); ;*/

        }
    }
}
