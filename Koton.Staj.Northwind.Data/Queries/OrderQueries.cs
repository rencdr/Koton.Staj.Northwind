

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class OrderQueries
    {
        internal const string  GET_ORDERS_BY_USER_ID_QUERY = "SELECT * FROM UserOrders WHERE UserId = @UserId";
        internal const string  INSERT_USER_ORDER_QUERY = @"
            INSERT INTO UserOrders (UserId, ProductId, Quantity, UserAddress, UserPhoneNumber, OrderDate)
            VALUES (@UserId, @ProductId, @Quantity, @UserAddress, @UserPhoneNumber, @OrderDate);
        ";

        internal const string CANCEL_USER_ORDER_QUERY = "DELETE FROM UserOrders WHERE OrderId = @OrderId ";
        internal const string GET_ORDER_BY_ID_QUERY = "SELECT * FROM UserOrders WHERE OrderId = @OrderId";
    }
}
