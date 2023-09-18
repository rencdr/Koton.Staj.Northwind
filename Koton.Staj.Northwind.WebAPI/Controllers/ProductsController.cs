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
            var response = _productService.GetAllProducts();

            return response.Success ? Ok(response) : BadRequest(response);
        }



        [HttpGet("byPriceRangeDesc")]
        public IActionResult GetAllProductsOrderByDescendingPrice()
        {
            var response = _productService.GetAllProductsOrderByDescendingPrice();

            return response.Success ? Ok(response) : BadRequest(response);
        }



        [HttpGet("byPriceRangeAsc")]
        public IActionResult GetAllProductsOrderByAsscendingPrice()
        {
            var response = _productService.GetAllProductsOrderByAscendingPrice();

            return response.Success ? Ok(response) : BadRequest(response);
        }



    }
}
