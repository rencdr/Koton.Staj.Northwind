using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities;
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

        [HttpGet("/api/products")]
        public IActionResult GetAllProducts()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProducts();
            return Ok(productDtos);
        }

        [HttpGet("/api/products/byPriceRangeDesc")]
        public IActionResult GetAllProductsOrderByDescendingPrice()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProductsOrderByDescendingPrice();
            return Ok(productDtos);
        }

        [HttpGet("/api/products/byPriceRangeAsc")]
        public IActionResult GetAllProductsOrderByAsscendingPrice()
        {
            IEnumerable<ProductDto> productDtos = _productService.GetAllProductsOrderByAscendingPrice();
            return Ok(productDtos);
        }
        //private readonly IProductService _productService;

        //public ProductsController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        //[HttpGet("/api/products")]
        //public IActionResult GetAllProducts()
        //{
        //    IEnumerable<Product> products = _productService.GetAllProducts();
        //    return Ok(products);
        //}

        //[HttpGet("/api/products/byPriceRangeDesc")]
        //public IActionResult GetAllProductsOrderByDescendingPrice()
        //{
        //    IEnumerable<Product> products = _productService.GetAllProductsOrderByDescendingPrice();
        //    return Ok(products);
        //}

        //[HttpGet("/api/products/byPriceRangeAsc")]
        //public IActionResult GetAllProductsOrderByAsscendingPrice()
        //{
        //    IEnumerable<Product> products = _productService.GetAllProductsOrderByAscendingPrice();
        //    return Ok(products);
        //}
    }
}
