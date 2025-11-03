using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications;
using Shared.DTOS;
using Shared.Enums;
using Shared.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    internal class ProductService(IUnitOfWork _unitOfWork ,IMapper _mapper) 
        : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDTO = _mapper.Map<IEnumerable<BrandDTO>>(brands);
            return brandsDTO;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(QueryProductParams queryParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var repo = _unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllAsync(spec);
            var productsDTO = _mapper.Map < IEnumerable< ProductDTO >> (products);
            return productsDTO;
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
           var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
           var TypesDTO = _mapper.Map<IEnumerable<TypeDTO>>(Types);
            return TypesDTO;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product =  await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(spec);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }
    }
}
