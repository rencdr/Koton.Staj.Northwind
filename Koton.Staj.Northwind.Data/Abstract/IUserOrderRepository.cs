using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserOrderRepository
    {
        void InsertUserOrder(UserOrder userOrder);

    }
}
