using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IOrderService
    {
        ResponseModel<int> CreateOrder(int userId, string userAddress, string userPhoneNumber);

        ResponseModel<List<UserOrder>> GetOrdersByUserId(int userId);

        ResponseModel<int> CancelOrder(int orderId);

        ResponseModel<bool> InsertUserOrder(UserOrder userOrder);

        ResponseModel<bool> UpdateCart(int cartId);

        ResponseModel<bool> UpdateCartByOrderId(UserOrder order);

        ResponseModel<bool> CancelUserOrder(UserOrder order);

    }
}
