using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared;
using Shared.DTOS.ProductDTOs;
using Shared.Enums;
using Shared.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize]
    public class ProductsController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]QueryProductParams queryParams)
        { 
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return  Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<BrandDTO>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<TypeDTO>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }


    }
}
