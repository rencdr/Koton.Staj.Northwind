

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class CartQueries
    {
        internal const string ADD_TO_CART_QUERY = @"
            INSERT INTO Carts (UserId, ProductId, Quantity, CreatedTime, OrderedTime, DeletedTime, IsActive, IsDeleted)
            VALUES (@UserId, @ProductId, @Quantity, GETDATE(), NULL, NULL, 1, 0)";
        internal const string GET_CART_ITEMS_QUERY = @"
           SELECT
    C.UserId,
    P.ProductId,
    P.ProductName,
    C.Quantity,
    P.UnitPrice,
    C.Quantity * P.UnitPrice AS TotalPrice,
    TotalCart.TotalCartAmount,
    Cat.CategoryName,
    Cat.Description,
    C.IsActive,
    C.IsDeleted
FROM Carts AS C
INNER JOIN Products AS P ON C.ProductId = P.ProductID
INNER JOIN Categories AS Cat ON P.CategoryID = Cat.CategoryID
OUTER APPLY (
    SELECT SUM(Cart.Quantity * Product.UnitPrice) AS TotalCartAmount
    FROM Carts AS Cart
    INNER JOIN Products AS Product ON Cart.ProductId = Product.ProductID
    WHERE Cart.UserId = C.UserId
) AS TotalCart
WHERE C.UserId = @UserId;
";

        internal const string REMOVE_FROM_CART_QUERY = @"
         DELETE FROM carts
         WHERE  userid = @UserId
         AND productid = @ProductId ";
        internal const string DELETE_CART_QUERY = @"
         DELETE FROM carts
         WHERE  userid = @UserId ";
        internal const string GET_CART_BY_USERID_QUERY = @"SELECT *
         FROM   carts
         WHERE  userid = @UserId
         AND orderedtime IS NULL ";
        internal const string UPDATE_CART_QUERY = @"
            UPDATE Carts
            SET IsActive = 1,
                OrderedTime = GETDATE()
            WHERE CartId = @CartId;
        ";
        internal const string  CARTS_IDS_QUERY = @"
            SELECT c.CartId
            FROM Carts c
            INNER JOIN UserOrders uo ON c.ProductId = uo.ProductId
            WHERE uo.OrderId = @OrderId;
        ";
        internal const string UPDATE_QUERY= @"
                UPDATE Carts
                SET IsDeleted = 1,
                    IsActive = 0,
                    DeletedTime = GETDATE()
                WHERE CartId IN @CartIds;
            ";
        internal const string CHECK_USER_EXIST_QUERY= @"
        SELECT COUNT(*) 
        FROM Users 
        WHERE UserId = @UserId";
    }
}
