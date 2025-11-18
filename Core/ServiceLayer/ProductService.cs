using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using DomainLayer.Models.ProductModule;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications;
using ServiceLayer.Specifications.ProductModuleSpecification;
using Shared;
using Shared.DTOS.ProductDTOs;
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

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(QueryProductParams queryParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var repo = _unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllAsync(spec);
            var productsDTO = _mapper.Map < IEnumerable< ProductDTO >> (products);

            var specificationOfCount = new ProductCountSpecification(queryParams);
            var countProductWithSpecification = await repo.GetCountSpecificationAsync(specificationOfCount);


            var paginatedProducts = new PaginatedResult<ProductDTO>
                (queryParams.PageSize, queryParams.PageIndex, countProductWithSpecification, productsDTO);
            return paginatedProducts;
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
            if (product == null) throw new ProductNotFoundException(id);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }
    }
}
