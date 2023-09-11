using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using System.Transactions;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using AutoMapper;
using Koton.Staj.Northwind.Business.Validation;

namespace Koton.Staj.Northwind.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserOrderRepository _userOrderRepository;
        private readonly IMapper _mapper;

        public OrderService(ICartRepository cartRepository, IUserOrderRepository userOrderRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _userOrderRepository = userOrderRepository;
            _mapper = mapper;

        }

       
        public ResponseModel CreateOrder(int userId, string userAddress, string userPhoneNumber)
        {
            var validator = new CreateOrderValidator();
            var orderRequest = new OrderRequestModel
            {
                UserId = userId,
                UserAddress = userAddress,
                UserPhoneNumber = userPhoneNumber
            };

            var validationResult = validator.Validate(orderRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new ResponseModel
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = errors
                };
            }

            IEnumerable<Cart> carts = _cartRepository.GetCartsByUserId(userId);

            if (carts != null && carts.Any())
            {
                using (var transactionScope = new TransactionScope())
                {
                    try
                    {
                        var userOrders = _mapper.Map<List<UserOrder>>(carts);

                        foreach (var userOrder in userOrders)
                        {
                            userOrder.UserAddress = userAddress;
                            userOrder.UserPhoneNumber = userPhoneNumber;
                            userOrder.UserId = userId;
                            UpdateCart(userOrder.CartId);
                            InsertUserOrder(userOrder);
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
                        Console.WriteLine("Hata Mesajı: " + ex.Message);

                        transactionScope.Dispose();
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




        public ResponseModel InsertUserOrder(UserOrder userOrder)
        {
            try
            {
                _userOrderRepository.InsertUserOrder(userOrder);

                return new ResponseModel
                {
                    Success = true,
                    Message = "User order inserted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir hata mesajı dönebilirsiniz.
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while inserting the user order: " + ex.Message,
                    Data = null
                };
            }
        }

       

        public ResponseModel UpdateCart(int cartId)
        {
            try
            {
                _cartRepository.UpdateCart(cartId);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Cart updated successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while updating the cart: " + ex.Message,
                    Data = null
                };
            }
        }

       



        public ResponseModel CancelOrder(int orderId)
        {
            Console.WriteLine("CancelOrder metoduna girildi.");
            Console.WriteLine("orderId: " + orderId);

            try
            {
                UserOrder orderToCancel = _userOrderRepository.GetOrderById(orderId);

                if (orderToCancel == null)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı.",
                        Data = null
                    };
                }

                DateTime orderUtcTime = TimeZoneInfo.ConvertTimeToUtc(orderToCancel.OrderDate);
                DateTime currentUtcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
                TimeSpan timeElapsed = currentUtcTime - orderUtcTime;

                if (timeElapsed.TotalHours > 3)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = "Sipariş 3 saatten fazla süre geçtiği için iptal edilemez.",
                        Data = null
                    };
                }
                UpdateCartByOrderId(orderToCancel);
                CancelUserOrder(orderToCancel);



                return new ResponseModel
                {
                    Success = true,
                    Message = "Sipariş iptal edildi ve sepet güncellendi.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Sipariş iptali sırasında bir hata oluştu: " + ex.Message,
                    Data = null
                };
            }
        }


        public ResponseModel UpdateCartByOrderId(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                _cartRepository.UpdateCartByOrderId(orderId);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Cart updated successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while updating the cart: " + ex.Message,
                    Data = null
                };
            }
        }

      

        public ResponseModel CancelUserOrder(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                _userOrderRepository.CancelUserOrder(orderId);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Order canceled successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while canceling the order: " + ex.Message,
                    Data = null
                };
            }
        }




        public List<UserOrder> GetOrdersByUserId(int userId)
        {
            return _userOrderRepository.GetOrdersByUserId(userId);
        }

        
        
    }
}






//public ResponseModel CreateOrder(int userId, string userAddress, string userPhoneNumber)
//{
//    IEnumerable<Cart> carts = _cartRepository.GetCartsByUserId(userId);

//    if (carts != null && carts.Any())
//    {
//        using (var transactionScope = new TransactionScope())
//        {
//            try
//            {
//                foreach (var cart in carts)
//                {
//                    UserOrder userOrder = new UserOrder
//                    {
//                        UserId = userId,
//                        Quantity = cart.Quantity,
//                        ProductId = cart.ProductId,
//                        UserAddress = userAddress,
//                        UserPhoneNumber = userPhoneNumber,
//                        OrderDate = DateTime.Now
//                    };

//                    InsertUserOrder(userOrder);
//                    UpdateCart(cart.CartId);

//                }

//                transactionScope.Complete();

//                return new ResponseModel
//                {
//                    Success = true,
//                    Message = Messages.ORDER_CREATED_SUCCESS,
//                    Data = null
//                };
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Hata Mesajı: " + ex.Message);

//                transactionScope.Dispose();
//                return new ResponseModel
//                {
//                    Success = false,
//                    Message = Messages.ORDER_CREATES_FAILED,
//                    Data = ex.Message
//                };
//            }
//        }
//    }
//    else
//    {
//        return new ResponseModel
//        {
//            Success = false,
//            Message = Messages.CART_NOT_FOUND,
//            Data = null
//        };
//    }
//}