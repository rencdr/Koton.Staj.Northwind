using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IOrderService
    {
        ResponseModel CreateOrder(int userId, string userAddress, string userPhoneNumber);
        List<UserOrder> GetOrdersByUserId(int userId);
        ResponseModel CancelOrder(int orderId);


    }
}
