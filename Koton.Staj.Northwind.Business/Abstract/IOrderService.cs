

using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IOrderService
    {
        Task<ResponseModel<int>> CreateOrderAsync(int userId, string userAddress, string userPhoneNumber);
        Task<ResponseModel<bool>> InsertUserOrderAsync(UserOrder userOrder);
        Task<ResponseModel<bool>> UpdateCartAsync(int cartId);
        Task<ResponseModel<List<UserOrder>>> GetOrdersByUserIdAsync(int userId);
        Task<ResponseModel<int>> CancelOrderAsync(int orderId);
        Task<ResponseModel<bool>> UpdateCartByOrderIdAsync(UserOrder order);
        Task<ResponseModel<bool>> CancelUserOrderAsync(UserOrder order);
    }
}

