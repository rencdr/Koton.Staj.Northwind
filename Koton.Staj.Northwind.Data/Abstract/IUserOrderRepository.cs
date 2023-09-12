using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserOrderRepository
    {
        bool InsertUserOrder(UserOrder order);

        //void InsertUserOrder(UserOrder order);
        List<UserOrder> GetOrdersByUserId(int userId);
        bool CancelUserOrder(int orderId);

        //void CancelUserOrder(int orderId);
        UserOrder GetOrderById(int orderId);


    }
}
