using AutoMapper;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl)) return string.Empty;

            else
            {
                var url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return url;
            }
        }
    }
}
