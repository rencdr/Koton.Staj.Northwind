
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using System.Data.SqlClient;
using Koton.Staj.Data.Abstract;

namespace Koton.Staj.Data.Concrete
{
    public class DapperCartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperCartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:SqlServerDb"];
        }

        public void AddToCart(Cart cartItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Carts (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)";
                connection.Execute(query, cartItem);

            }
        }
        public List<Cart> GetCartItems(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
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

                return connection.Query<Cart>(query, new { UserId = userId }).ToList();
            }


        }

        public void RemoveFromCart(int userId, int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM Carts WHERE UserId = @UserId AND ProductId = @ProductId";
                connection.Execute(query, new { UserId = userId, ProductId = productId });
            }
        }
    }
}
