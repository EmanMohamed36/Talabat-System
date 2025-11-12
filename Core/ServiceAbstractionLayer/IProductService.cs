using Shared;
using Shared.DTOS.ProductDTOs;
using Shared.Enums;
using Shared.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(QueryProductParams queryParams);

        Task<ProductDTO> GetProductByIdAsync(int id);

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

    }
}
