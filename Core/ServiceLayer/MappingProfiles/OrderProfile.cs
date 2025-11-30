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
                                               .ForMember(dist => dist.Status
                                               ,option => option.MapFrom(src => src.orderStatus.ToString()))
                                               .ForMember(dist => dist.DeliveryMethod
                                               , option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                                               .ForMember(dist => dist.BuyerEmail
                                               , option => option.MapFrom(src => src.UserEmail))
                                               .ForMember(dist => dist.shipToAddress
                                               , option => option.MapFrom(src => src.OrderAddress))
                                               .ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ForMember(dist => dist.ProductName, option => option.MapFrom(src => src.productItemOrdered.ProductName))
                                                .ForMember(dist => dist.PictureUrl, option => option.MapFrom<OrderItemPictureUrlResolver>())
                                                .ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDTO>()
                                                    .ForMember(dist => dist.Cost , opt => opt.MapFrom(src => src.Price))    
                                                    .ReverseMap();
        }
    }
}
