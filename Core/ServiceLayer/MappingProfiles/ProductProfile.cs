using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DTOS.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ServiceLayer.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                    .ForMember(des => des.BrandName,
                    options => options.MapFrom(src => src.ProductBrand.Name))
                    .ForMember(des => des.TypeName,
                    options => options.MapFrom(src => src.ProductType.Name))
                    .ForMember(des => des.PictureUrl, 
                    options => options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();

        }
    }
}
