using DomainLayer.Models.ProductModule;
using Shared.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications.ProductModuleSpecification
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(QueryProductParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                      && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                      && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue)))

        {
        }
       
    }
}
