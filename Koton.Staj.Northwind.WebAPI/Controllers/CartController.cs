using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities.Dtos;
using FluentValidation;

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
            var result = _cartService.AddToCart(cartItem);

            if (!result.Success)
            {
                return BadRequest(new { Message = "Validation error", Errors = result.Errors });
            }

            return Ok(new { Message = "Product added to cart successfully", CartId = result.CartId });
        }




        [HttpGet("getCartItemsByUserId")]
        public IActionResult GetCartItems([FromQuery] int userId)
        {
            try
            {
                var cartItems = _cartService.GetCartItems(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred: " + ex.Message });
            }


        }

        [HttpDelete("removeProductFromCart")]
        public IActionResult RemoveFromCart(int userId, int productId)
        {
            try
            {
                _cartService.RemoveFromCart(userId, productId);
                return Ok(new { Message = "Product removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred: " + ex.Message });
            }
        }
        
        [HttpDelete("clearCartByUserId")]
        public IActionResult ClearCart(int userId)
        {
            try
            {
                _cartService.DeleteCartByUserId(userId);
                return Ok(new { Message = "Cart cleared successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred: " + ex.Message });
            }
        }
    }
}
