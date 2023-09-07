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

        public OrderService(ICartRepository cartRepository, IUserOrderRepository userOrderRepository)
        {
            _cartRepository = cartRepository;
            _userOrderRepository = userOrderRepository;
        }




        public ResponseModel CreateOrder(int userId, string userAddress, string userPhoneNumber)
        {
            IEnumerable<Cart> carts = _cartRepository.GetCartsByUserId(userId);

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
                                UserId = userId,
                                Quantity = cart.Quantity,
                                ProductId = cart.ProductId,
                                UserAddress = userAddress,
                                UserPhoneNumber = userPhoneNumber,
                                OrderDate = DateTime.Now
                            };

                            // Siparişi UserOrders tablosuna ekle
                            _userOrderRepository.InsertUserOrder(userOrder);
                            _cartRepository.UpdateCart(cart.CartId);
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




        public ResponseModel CancelOrder(int orderId)
        {
            Console.WriteLine("CancelOrder metoduna girildi.");
            Console.WriteLine("orderId: " + orderId); // orderId'yi ekrana yazdır

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

                // İptal işlemi başarılı olduysa, Cart güncellemesini yap
                _cartRepository.UpdateCartByOrderId(orderId);
                // Eğer sipariş iptal edilebilirse, iptal işlemini gerçekleştir
                _userOrderRepository.CancelUserOrder(orderId);



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



        public List<UserOrder> GetOrdersByUserId(int userId)
        {
            return _userOrderRepository.GetOrdersByUserId(userId);
        }


    }
}





