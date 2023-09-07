using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;

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
        public IActionResult AddToCart(AddToCartDto cartItem)
        {

            try
            {
                int userId = cartItem.UserId;
                int productId = cartItem.ProductId;
                int quantity = cartItem.Quantity;
                _cartService.AddToCart(cartItem);

                return Ok(new { Message = "Product added to cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred: " + ex.Message });
            }
        }



        [HttpGet("getCartItemsByUserId")]
        public IActionResult GetCartItems([FromQuery]int userId)
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
