using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Business.Utilities;

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

        [HttpPost("createOrder")]
        public IActionResult CreateOrder([FromBody] OrderRequestModel request)
        {
            var result = _orderService.CreateOrder(request.UserId, request.UserAddress, request.UserPhoneNumber);

            if (result.Success)
            {
                return Ok(new { Message = result.Message });
            }
            else
            {
                return BadRequest(new { Message = result.Message, Errors = result.Data });
            }
        }



        [HttpGet("getOrdersByUserId")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _userOrderRepository.GetOrdersByUserId(userId);

            var response = new ResponseModel { Success = orders != null };

            if (orders != null)
            {
                response.Data = orders.ToList();
            }

            return Ok(response);
        }






        [HttpDelete("cancelOrderByOrderId")]

        public IActionResult CancelOrder([FromQuery] int orderId)
        {
            var order = _userOrderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            var result = _orderService.CancelOrder(orderId);

            var response = new ResponseModel
            {
                Success = result.Success,
                Message = result.Message
            };

            return Ok(response);
        }



    }

}
