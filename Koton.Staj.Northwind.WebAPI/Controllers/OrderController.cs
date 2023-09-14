using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Business.Concrete;

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
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpGet("getOrdersByUserId")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var result = _orderService.GetOrdersByUserId(userId);

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpDelete("cancelOrderByOrderId")]
        public IActionResult CancelOrder([FromQuery] int orderId)
        {
            var result = _orderService.CancelOrder(orderId);

            return result.Success ? Ok(result) : BadRequest(result);

        }




    }

}
