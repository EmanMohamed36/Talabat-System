using AutoMapper;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.productItemOrdered.PictureUrl)) return string.Empty;

            else
            {
                var url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.productItemOrdered.PictureUrl}";
                return url;
            }
        }
    }
}
