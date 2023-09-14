using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserOrderRepository
    {
        bool InsertUserOrder(UserOrder order);
        int GetLastInsertedOrderId(int userId);
        List<UserOrder> GetOrdersByUserId(int userId);
        bool CancelUserOrder(int orderId);
        UserOrder GetOrderById(int orderId);


    }
}
