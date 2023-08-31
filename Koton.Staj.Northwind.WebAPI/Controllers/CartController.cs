using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Business.Abstract;


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
        public IActionResult AddToCart(Cart cartItem)
        {

            try
            {
                _cartService.AddToCart(cartItem);
                return Ok(new { Message = "Product added to cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred: " + ex.Message });
            }
        }


        [HttpGet("displayCart{userId}")]
        public IActionResult GetCartItems(int userId)
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
    }
}
