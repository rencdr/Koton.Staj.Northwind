using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities.Dtos;
using Koton.Staj.Northwind.Business.Utilities;
using FluentValidation;
using System.Collections.Generic;


namespace Koton.Staj.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }



        [HttpPost("addProductToCart")]
        public IActionResult AddToCart([FromBody] AddToCartDto cartItem)
        {
            ResponseModel<List<string>> response = _cartService.AddToCart(cartItem);

            return response.Success ? Ok(response) : BadRequest(response);

        }




        [HttpGet("getCartItemsByUserId")]
        public IActionResult GetCartItems([FromQuery] int userId)
        {
            ResponseModel<List<DisplayCartDto>> response = _cartService.GetCartItems(userId);

            return response.Success ? Ok(response) : BadRequest(response);

        }




        [HttpDelete("removeProductFromCart")]
        public IActionResult RemoveFromCart(int userId, int productId)
        {
            ResponseModel<bool> response = _cartService.RemoveFromCart(userId, productId);

            return response.Success ? Ok(response) : BadRequest(response);

        }




        [HttpDelete("clearCartByUserId")]
        public IActionResult ClearCart([FromQuery] int userId)
        {
            ResponseModel<bool> response = _cartService.DeleteCartByUserId(userId);
            return response.Success ? Ok(response) : BadRequest(response);

        }


    }
}
