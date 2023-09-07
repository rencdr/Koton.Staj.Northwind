using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserOrderRepository
    {
        void InsertUserOrder(UserOrder order);
        List<UserOrder> GetOrdersByUserId(int userId);
        void CancelUserOrder(int orderId);
        UserOrder GetOrderById(int orderId);


    }
}
