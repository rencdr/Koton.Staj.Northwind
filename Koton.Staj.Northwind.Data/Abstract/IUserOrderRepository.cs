


using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserOrderRepository
    {
        Task<ResponseModel<bool>> InsertUserOrderAsync(UserOrder order);

        Task<ResponseModel<int>> GetLastInsertedOrderIdAsync(int userId);

        Task<ResponseModel<List<UserOrder>>> GetOrdersByUserIdAsync(int userId);

        Task<ResponseModel<bool>> CancelUserOrderAsync(int orderId);

        Task<ResponseModel<UserOrder>> GetOrderByIdAsync(int orderId);

    }
}
