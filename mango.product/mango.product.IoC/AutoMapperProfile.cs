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

            //OUT
            /*CreateMap<ordering.domain.models.Order, ordering.application.models.Order>();
            CreateMap<ordering.domain.models.PaymentMethod, ordering.application.models.PaymentMethod>();

            // IN
            CreateMap<ordering.application.models.Order, ordering.domain.models.Order>();
            CreateMap<ordering.application.models.PaymentMethod, ordering.domain.models.PaymentMethod>();


            //Event Bus
            CreateMap<infra.eventbus.events.BasketCartCheckoutEvent, ordering.application.models.Order>().ReverseMap(); ;*/

        }
    }
}
