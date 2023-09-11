using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Koton.Staj.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("randomList")]
        public IActionResult GetAllProducts()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProducts();
            return Ok(productDtos);
        }

        [HttpGet("/byPriceRangeDesc")]
        public IActionResult GetAllProductsOrderByDescendingPrice()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProductsOrderByDescendingPrice();
            return Ok(productDtos);
        }

        [HttpGet("byPriceRangeAsc")]
        public IActionResult GetAllProductsOrderByAsscendingPrice()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProductsOrderByAscendingPrice();
            return Ok(productDtos);
        }
        
    }
}
