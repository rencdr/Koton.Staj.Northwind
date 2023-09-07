using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;
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
        public IActionResult CreateOrder(OrderRequestModel request)
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

        [HttpGet("getOrdersByUserId")]
        public IActionResult GetOrdersByUserId( int userId)
        {
            var orders = _userOrderRepository.GetOrdersByUserId(userId);

            if (orders == null)
            {
                return NotFound(); //  404 döndür
            }

            return Ok(orders); //  200 OK  döndür
        }

 
        [HttpDelete("cancelOrderByOrderId")]
        public IActionResult CancelOrder([FromQuery] int orderId)
        {
            Console.WriteLine("Cancel isteğine yapıldı."); //

            var order = _userOrderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            var result = _orderService.CancelOrder(orderId);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


    }

}
