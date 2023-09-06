using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;

namespace Koton.Staj.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserOrderRepository _userOrderRepository;

        public OrderController(IOrderService orderService, IUserOrderRepository userOrderRepository)
        {
            _orderService = orderService;
            _userOrderRepository = userOrderRepository;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderRequestModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            int userId = request.UserId;
            string userAddress = request.UserAddress;
            string userPhoneNumber = request.UserPhoneNumber;

            var result = _orderService.CreateOrder(userId, userAddress, userPhoneNumber);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _userOrderRepository.GetOrdersByUserId(userId);

            if (orders == null)
            {
                return NotFound(); //  404 döndür
            }

            return Ok(orders); //  200 OK  döndür
        }
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            var order = _userOrderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            try
            {
                _userOrderRepository.CancelUserOrder(orderId);

                return Ok("Order canceled successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to cancel order: {ex.Message}");
            }
        }


    }

}
