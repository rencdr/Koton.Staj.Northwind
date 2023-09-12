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
                string query = Queries.OrderQueries.GET_ORDERS_BY_USER_ID_QUERY;
                return dbConnection.Query<UserOrder>(query, new { UserId = userId }).ToList();
            }
        }

        public bool InsertUserOrder(UserOrder order)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string insertQuery = Queries.OrderQueries.INSERT_USER_ORDER_QUERY;

                    int rowsAffected = dbConnection.Execute(insertQuery, order);

                    // Eğer en az bir satır ekleniyorsa işlem başarılı sayılır.
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void InsertUserOrder(UserOrder order)
        //{
        //    using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //    {
        //        string insertQuery = Queries.OrderQueries.INSERT_USER_ORDER_QUERY;


        //        dbConnection.Execute(insertQuery, order);
        //    }
        //}
        //void değil değer döndür

        public bool CancelUserOrder(int orderId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string deleteQuery = Queries.OrderQueries.CANCEL_USER_ORDER_QUERY;
                    int rowsAffected = dbConnection.Execute(deleteQuery, new { OrderId = orderId });

                    // Eğer en az bir satır siliniyorsa işlem başarılı sayılır.
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void CancelUserOrder(int orderId)
        //{
        //    using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //    {
        //        string deleteQuery = Queries.OrderQueries.CANCEL_USER_ORDER_QUERY;
        //        dbConnection.Execute(deleteQuery, new { OrderId = orderId });
        //    }
        //}

        public UserOrder GetOrderById(int orderId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string query = Queries.OrderQueries.GET_ORDER_BY_ID_QUERY;
                return dbConnection.QueryFirstOrDefault<UserOrder>(query, new { OrderId = orderId });
            }
        }


    }
}
    
