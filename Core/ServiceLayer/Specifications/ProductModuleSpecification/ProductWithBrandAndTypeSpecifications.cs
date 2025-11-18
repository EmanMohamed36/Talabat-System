using DomainLayer.Models.ProductModule;
using Shared.Enums;
using Shared.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications.ProductModuleSpecification
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(QueryProductParams queryParams) 
            : base(p => (!queryParams.BrandId.HasValue ||p.BrandId == queryParams.BrandId) 
                        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                        && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue )))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBY(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderBYDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBY(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderBYDescending(p => p.Price);
                    break;
                default:
                    break;

            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
