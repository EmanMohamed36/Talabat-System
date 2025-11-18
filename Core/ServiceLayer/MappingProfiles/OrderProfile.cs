using AutoMapper;
using DomainLayer.Models.OrderModels;
using Shared.DTOS.IdentityDTOs;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, ShippingAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                                               .ForMember(dist => dist.orderStatus
                                               ,option => option.MapFrom(src => src.orderStatus.ToString()))
                                               .ForMember(dist => dist.DeliveryMethodName
                                               , option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                                               .ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ForMember(dist => dist.ProductName, option => option.MapFrom(src => src.productItemOrdered.ProductName))
                                                .ForMember(dist => dist.PictureUrl, option => option.MapFrom<OrderItemPictureUrlResolver>())
                                                .ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();
        }
    }
}
