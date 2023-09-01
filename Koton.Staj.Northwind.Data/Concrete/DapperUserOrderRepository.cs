using System;
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

        public void InsertUserOrder(UserOrder userOrder)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
INSERT INTO UserOrders (CartId, UserId,  Quantity, ProductId, UserAddress, UserPhoneNumber, OrderDate)
VALUES (@CartId, @UserId, @Quantity, @ProductId, @UserAddress, @UserPhoneNumber, @OrderDate)";


            connection.Execute(query, userOrder);
        }
  
    }
}
