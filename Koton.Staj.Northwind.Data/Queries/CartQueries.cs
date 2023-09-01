using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class CartQueries
    {
        internal const string ADD_TO_CART_QUERY = @"INSERT INTO Carts (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)";
        internal const string GET_CART_ITEMS_QUERY = @"
    SELECT C.UserId, P.ProductId, P.ProductName, C.Quantity, P.UnitPrice,
           C.Quantity * P.UnitPrice AS TotalPrice,
           TotalCart.TotalCartAmount,
           Cat.CategoryName, Cat.Description
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
        internal const string REMOVE_FROM_CART_QUERY = @"DELETE FROM Carts WHERE UserId = @UserId AND ProductId = @ProductId";
        internal const string DELETE_CART_QUERY = "DELETE FROM Carts WHERE UserId = @UserId";
        internal const string GET_CART_BY_USERID_QUERY = "DELETE FROM Carts WHERE UserId = @UserId";
    }
}
