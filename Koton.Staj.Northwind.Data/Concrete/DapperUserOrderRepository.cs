using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;

namespace Koton.Staj.Northwind.Data.Concrete
{

    public class DapperUserOrderRepository : IUserOrderRepository
    {
        private readonly string _connectionString;

        public DapperUserOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
        }



        public List<UserOrder> GetOrdersByUserId(int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM UserOrders WHERE UserId = @UserId";
                return dbConnection.Query<UserOrder>(query, new { UserId = userId }).ToList();
            }
        }

        public void InsertUserOrder(UserOrder order)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string insertQuery = @"
    INSERT INTO UserOrders (CartId, UserId, Quantity, UserAddress, UserPhoneNumber, OrderDate, ProductId)
    VALUES (@CartId, @UserId, @Quantity, @UserAddress, @UserPhoneNumber, @OrderDate, @ProductId);
";


                dbConnection.Execute(insertQuery, order);
            }
        }

        public void CancelUserOrder(int orderId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string deleteQuery = "DELETE FROM UserOrders WHERE OrderId = @OrderId ";
                dbConnection.Execute(deleteQuery, new { OrderId = orderId });
            }
        }

        public UserOrder GetOrderById(int orderId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM UserOrders WHERE OrderId = @OrderId";
                return dbConnection.QueryFirstOrDefault<UserOrder>(query, new { OrderId = orderId });
            }
        }
    }
}
    
