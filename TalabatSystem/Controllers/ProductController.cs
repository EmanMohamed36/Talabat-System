using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatSystem.Models;

namespace TalabatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id}")] //BaseUrl/api/product
        public ActionResult<Product> Get(int id)
        { 
            return new Product { Id = id };
        }
        [HttpGet] //BaseUrl/api/product
        public ActionResult<Product> GetAll()
        {
            return new Product { Id = 20 , Name = "Cheeese"};
        }

        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            return product;
        }

        [HttpPost("Brand")]
        public ActionResult<Product> AddBrand(Product product)
        {
            return product;
        }
        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            return product;
        }
        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product product)
        {
            return product;
        }

    }
}
