using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Business.Concrete;
using Koton.Staj.Northwind.Entities.Concrete;

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
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestModel request)
        {
            var result = await _orderService.CreateOrderAsync(request.UserId, request.UserAddress, request.UserPhoneNumber);
            return result.Success ? Ok(result) : BadRequest(result);
        }



        [HttpGet("getOrdersByUserId")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var result = await _orderService.GetOrdersByUserIdAsync(userId);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        

        [HttpDelete("cancelOrderByOrderId")]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);

            return result.Success ? Ok(result) : BadRequest(result);
        }
       


    }

}
