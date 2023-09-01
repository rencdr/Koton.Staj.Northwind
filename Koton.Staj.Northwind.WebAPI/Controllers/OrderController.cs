﻿using Koton.Staj.Northwind.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Koton.Staj.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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

            _orderService.CreateOrder(userId, userAddress, userPhoneNumber);

            return Ok("Order created successfully");
        }
    }

    public class OrderRequestModel
    {
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}