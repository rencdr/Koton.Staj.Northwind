using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;
using System.Transactions;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;

namespace Koton.Staj.Northwind.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserOrderRepository _userOrderRepository;
        private readonly string _connectionString;
        private readonly ICartService _cartService;

        public OrderService(ICartRepository cartRepository, IUserOrderRepository userOrderRepository, IConfiguration configuration, ICartService cartService)
        {
            _cartRepository = cartRepository;
            _userOrderRepository = userOrderRepository;
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
            _cartService = cartService;
        }

        public ResponseModel CreateOrder(int userId, string userAddress, string userPhoneNumber)
        {
            IEnumerable<Cart> carts = _cartService.GetCartsByUserId(userId);

            if (carts != null && carts.Any())
            {
                using (var transactionScope = new TransactionScope())
                {
                    try
                    {
                        foreach (var cart in carts)
                        {
                            UserOrder userOrder = new UserOrder
                            {
                                CartId = cart.CartId,
                                UserId = userId,
                                Quantity = cart.Quantity,
                                ProductId = cart.ProductId,// yeni eklenen sütun
                                UserAddress = userAddress,
                                UserPhoneNumber = userPhoneNumber,
                                OrderDate = DateTime.Now
                            };

                            _userOrderRepository.InsertUserOrder(userOrder);

                            _cartRepository.RemoveFromCart(userId, cart.ProductId);
                        }

                        transactionScope.Complete();

                        return new ResponseModel
                        {
                            Success = true,
                            Message = Messages.ORDER_CREATED_SUCCESS,
                            Data = null
                        };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel
                        {
                            Success = false,
                            Message = Messages.ORDER_CREATES_FAILED,
                            Data = ex.Message 
                        };
                    }
                }
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = Messages.CART_NOT_FOUND,
                    Data = null
                };
            }
        }
    }
}





