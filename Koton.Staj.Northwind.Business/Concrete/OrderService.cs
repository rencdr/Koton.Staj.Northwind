using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using System.Transactions;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using AutoMapper;
using Koton.Staj.Northwind.Business.Validation;
using System.Data.SqlClient;

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




        //async yap
        public ResponseModel<int> CreateOrder(int userId, string userAddress, string userPhoneNumber)
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
                return new ResponseModel<int>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = default(int)
                };
            }
            List<Cart> carts = _cartRepository.GetCartsByUserId(userId).ToList();

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

                        int orderId = _userOrderRepository.GetLastInsertedOrderId(userId);

                        transactionScope.Complete();

                        return new ResponseModel<int>
                        {
                            Success = true,
                            Message = Messages.ORDER_CREATED_SUCCESS,
                            Data = orderId
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata Mesajı: " + ex.Message);

                        transactionScope.Dispose();
                        return new ResponseModel<int>
                        {
                            Success = false,
                            Message = Messages.ORDER_CREATES_FAILED,
                            Data = 0
                        };
                    }
                }
            }
            else
            {
                return new ResponseModel<int>
                {
                    Success = false,
                    Message = Messages.CART_NOT_FOUND,
                    Data = 0
                };
            }
        }




        public ResponseModel<bool> InsertUserOrder(UserOrder userOrder)
        {
            try
            {
                _userOrderRepository.InsertUserOrder(userOrder);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Sipariş başarıyla eklendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Sipariş eklenirken bir hata oluştu.",
                    Data = false
                };
            }
        }




        public ResponseModel<bool> UpdateCart(int cartId)
        {
            try
            {
                _cartRepository.UpdateCart(cartId);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Sepet güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Sepet güncellenirken bir hata oluştu.",
                    Data = false
                };
            }
        }



        public ResponseModel<List<UserOrder>> GetOrdersByUserId(int userId)
        {
            try
            {
                List<UserOrder> orders = _userOrderRepository.GetOrdersByUserId(userId);
                return new ResponseModel<List<UserOrder>>
                {
                    Success = true,
                    Message = "Siparişler başarıyla alındı.",
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<List<UserOrder>>
                {
                    Success = false,
                    Message = "Siparişler alınırken bir hata oluştu.",
                    Data = null
                };
            }
        }


        public ResponseModel<int> CancelOrder(int orderId)
        {
            Console.WriteLine("CancelOrder metoduna girildi.");
            Console.WriteLine("orderId: " + orderId);

            UserOrder orderToCancel = _userOrderRepository.GetOrderById(orderId);

            if (orderToCancel == null)
            {
                return new ResponseModel<int>
                {
                    Success = false,
                    Message = "Sipariş bulunamadı.",
                    Data = -1 // Hata 
                };
            }

            DateTime orderUtcTime = TimeZoneInfo.ConvertTimeToUtc(orderToCancel.OrderDate);
            DateTime currentUtcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            TimeSpan timeElapsed = currentUtcTime - orderUtcTime;

            if (timeElapsed.TotalHours > 3)
            {
                return new ResponseModel<int>
                {
                    Success = false,
                    Message = "Sipariş 3 saatten fazla süre geçtiği için iptal edilemez.",
                    Data = -2 // Hata 
                };
            }

            try
            {
                UpdateCartByOrderId(orderToCancel);
                CancelUserOrder(orderToCancel);

                return new ResponseModel<int>
                {
                    Success = true,
                    Message = "Sipariş iptal edildi ve sepet güncellendi.",
                    Data = orderId // Başarılı
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Success = false,
                    Message = "Sipariş iptali sırasında bir hata oluştu: " + ex.Message,
                    Data = -3 // Hata 
                };
            }
        }


  


        public ResponseModel<bool> UpdateCartByOrderId(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                _cartRepository.UpdateCartByOrderId(orderId);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Sepet güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Sepet güncellenirken bir hata oluştu.",
                    Data = false
                };
            }
        }




        public ResponseModel<bool> CancelUserOrder(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                _userOrderRepository.CancelUserOrder(orderId);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Sipariş iptal edildi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Sipariş iptali sırasında bir hata oluştu.",
                    Data = false
                };
            }
        }


    }
}






