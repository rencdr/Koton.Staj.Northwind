

using Koton.Staj.Northwind.Data.Abstract;
using System.Transactions;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using AutoMapper;
using Koton.Staj.Northwind.Business.Validation;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Koton.Staj.Northwind.Entities.Concrete;

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

        public async Task<ResponseModel<int>> CreateOrderAsync(int userId, string userAddress, string userPhoneNumber)
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
                return new ResponseModel<int> { Success = false, Message = Messages.VALIDATION_FAILED, Data = 0 };
            }

            var cartsResponse = await _cartRepository.GetCartsByUserIdAsync(userId);

            if (!cartsResponse.Success || cartsResponse.Data == null || !cartsResponse.Data.Any())
            {
                return new ResponseModel<int> { Success = false, Message = Messages.CART_NOT_FOUND, Data = 0 };
            }

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var userOrders = _mapper.Map<List<UserOrder>>(cartsResponse.Data);

                    foreach (var userOrder in userOrders)
                    {
                        userOrder.UserAddress = userAddress;
                        userOrder.UserPhoneNumber = userPhoneNumber;
                        userOrder.UserId = userId;
                        var updateCartResponse = await UpdateCartAsync(userOrder.CartId);
                        if (!updateCartResponse.Success)
                        {
                            transactionScope.Dispose();
                            return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CREATES_FAILED, Data = 0 };
                        }
                        var insertUserOrderResponse = await InsertUserOrderAsync(userOrder);
                        if (!insertUserOrderResponse.Success)
                        {
                            transactionScope.Dispose();
                            return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CREATES_FAILED, Data = 0 };
                        }
                    }

                    var orderIdResponse = await _userOrderRepository.GetLastInsertedOrderIdAsync(userId);

                    if (orderIdResponse.Success)
                    {
                        int orderId = orderIdResponse.Data;
                        transactionScope.Complete();

                        return new ResponseModel<int> { Success = true, Message = Messages.ORDER_CREATED_SUCCESS, Data = orderId };
                    }
                    else
                    {
                        transactionScope.Dispose();
                        return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CREATES_FAILED + ": " + orderIdResponse.Message, Data = 0 };
                    }
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CREATES_FAILED + ": " + ex.Message, Data = 0 };
                }
            }
        }




        public async Task<ResponseModel<bool>> InsertUserOrderAsync(UserOrder userOrder)
        {
            try
            {
                await _userOrderRepository.InsertUserOrderAsync(userOrder);
                return new ResponseModel<bool> { Success = true, Message = Messages.ORDER_ADDED_SUCCESS, Data = true };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.ORDER_ADD_ERROR + ": " + ex.Message, Data = false };
            }
        }

        public async Task<ResponseModel<bool>> UpdateCartAsync(int cartId)
        {
            try
            {
                await _cartRepository.UpdateCartAsync(cartId);
                return new ResponseModel<bool> { Success = true, Message = Messages.CART_UPDATED_SUCCESS, Data = true };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.CART_UPDATE_ERROR + ": " + ex.Message, Data = false };
            }
        }

        public async Task<ResponseModel<List<UserOrder>>> GetOrdersByUserIdAsync(int userId)
        {
            try
            {
                var orderResponse = await _userOrderRepository.GetOrdersByUserIdAsync(userId);

                if (orderResponse.Success)
                {
                    List<UserOrder> orders = orderResponse.Data;
                    return new ResponseModel<List<UserOrder>> { Success = true, Message = Messages.ORDERS_RETRIEVED_SUCCESS, Data = orders };
                }
                else
                {
                    return new ResponseModel<List<UserOrder>> { Success = false, Message = Messages.ORDERS_NOT_FOUND, Data = null };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<UserOrder>> { Success = false, Message = Messages.ORDERS_RETRIEVAL_ERROR + ": " + ex.Message, Data = null };
            }
        }





        public async Task<ResponseModel<int>> CancelOrderAsync(int orderId)
        {
            try
            {
                var orderResponse = await _userOrderRepository.GetOrderByIdAsync(orderId);

                if (!orderResponse.Success || orderResponse.Data == null)
                {
                    return new ResponseModel<int> { Success = false, Message = Messages.ORDER_NOT_FOUND, Data = -1 };
                }

                UserOrder orderToCancel = orderResponse.Data;

                DateTime orderUtcTime = TimeZoneInfo.ConvertTimeToUtc(orderToCancel.OrderDate);
                DateTime currentUtcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
                TimeSpan timeElapsed = currentUtcTime - orderUtcTime;

                if (timeElapsed.TotalHours > 3)
                {
                    return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CANCEL_TIME_EXCEEDED, Data = -2 };
                }

                try
                {
                    await UpdateCartByOrderIdAsync(orderToCancel);
                    await CancelUserOrderAsync(orderToCancel);

                    return new ResponseModel<int> { Success = true, Message = Messages.ORDER_CANCELLED_AND_CART_UPDATED, Data = orderId };
                }
                catch (Exception ex)
                {
                    return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CANCEL_ERROR + ": " + ex.Message, Data = -3 };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Success = false, Message = Messages.ORDER_CANCEL_ERROR + ": " + ex.Message, Data = -3 };
            }
        }


        public async Task<ResponseModel<bool>> UpdateCartByOrderIdAsync(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                await _cartRepository.UpdateCartByOrderIdAsync(orderId);
                return new ResponseModel<bool> { Success = true, Message = Messages.CART_UPDATED_SUCCESS, Data = true };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.CART_UPDATE_ERROR + ": " + ex.Message, Data = false };
            }
        }

        public async Task<ResponseModel<bool>> CancelUserOrderAsync(UserOrder order)
        {
            try
            {
                int orderId = order.OrderId;
                await _userOrderRepository.CancelUserOrderAsync(orderId);
                return new ResponseModel<bool> { Success = true, Message = Messages.ORDER_CANCELLED, Data = true };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.ORDER_CANCEL_ERROR + ": " + ex.Message, Data = false };
            }
        }
    }
}





