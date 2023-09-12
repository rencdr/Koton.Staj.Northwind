
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using System.Data.SqlClient;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Data.Queries;
using System.Data;

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

        public bool AddToCart(Cart cartItem)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = CartQueries.ADD_TO_CART_QUERY;
                    connection.Execute(query, cartItem);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void AddToCart(Cart cartItem)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var query = CartQueries.ADD_TO_CART_QUERY;
        //        connection.Execute(query, cartItem);

        //    }
        //}
        public List<Cart> GetCartItems(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.GET_CART_ITEMS_QUERY;

                return connection.Query<Cart>(query, new { UserId = userId }).ToList();
            }


        }

        public bool RemoveFromCart(int userId, int productId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = CartQueries.REMOVE_FROM_CART_QUERY;
                    connection.Execute(query, new { UserId = userId, ProductId = productId });
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void RemoveFromCart(int userId, int productId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var query = CartQueries.REMOVE_FROM_CART_QUERY;
        //        connection.Execute(query, new { UserId = userId, ProductId = productId });
        //    }
        //}


        public bool DeleteCartByUserId(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = CartQueries.DELETE_CART_QUERY;
                    connection.Execute(query, new { UserId = userId });
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void DeleteCartByUserId(int userId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var query = CartQueries.DELETE_CART_QUERY;
        //        connection.Execute(query, new { UserId = userId });
        //    }
        //}


        public Cart GetCartByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = CartQueries.GET_CART_BY_USERID_QUERY;
            return connection.QueryFirstOrDefault<Cart>(query, new { UserId = userId });
        }

        public List<Cart> GetCartsByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.GET_CART_BY_USERID_QUERY;
                var carts = connection.Query<Cart>(query, new { UserId = userId });
                return carts.ToList();
            }
        }

        //public IEnumerable<Cart> GetCartsByUserId(int userId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var query = CartQueries.GET_CART_BY_USERID_QUERY;
        //        return connection.Query<Cart>(query, new { UserId = userId });
        //    }
        //}


        public bool UpdateCart(int cartId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string updateQuery = CartQueries.UPDATE_CART_QUERY;
                    int rowsAffected = dbConnection.Execute(updateQuery, new { CartId = cartId });

                    // Eğer en az bir satır güncellendiyse işlem başarılı sayılır.
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }

        //public void UpdateCart(int cartId)
        //{
        //    Console.WriteLine("Update Cart Girildi");
        //    using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //    {
        //        string updateQuery = CartQueries.UPDATE_CART_QUERY;

        //        dbConnection.Execute(updateQuery, new { CartId = cartId });
        //    }

        //}


        public bool UpdateCartByOrderId(int orderId)
        {
            try
            {
                Console.WriteLine("UpdateCartByOrderId");

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    // CartIds'yi sorgula
                    string cartIdsQuery = CartQueries.CARTS_IDS_QUERY;

                    IEnumerable<int> cartIds = dbConnection.Query<int>(cartIdsQuery, new { OrderId = orderId });

                    if (cartIds.Any())
                    {
                        // Carts tablosunu güncelle
                        string updateQuery = CartQueries.UPDATE_QUERY;

                        int rowsAffected = dbConnection.Execute(updateQuery, new { CartIds = cartIds });

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Cart güncellendi");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Cart güncellenemedi veya hiçbir satır etkilenmedi.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cart bulunamadı.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return false;
            }
        }


        //public void UpdateCartByOrderId(int orderId)
        //{
        //    Console.WriteLine("UpdateCartByOrderId");

        //    using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //    {
        //        // CartIds'yi sorgula
        //        string cartIdsQuery = CartQueries.CARTS_IDS_QUERY;

        //        IEnumerable<int> cartIds = dbConnection.Query<int>(cartIdsQuery, new { OrderId = orderId });

        //        if (cartIds.Any())
        //        {
        //            // Carts tablosunu güncelle
        //            string updateQuery = CartQueries.UPDATE_QUERY;

        //            int rowsAffected = dbConnection.Execute(updateQuery, new { CartIds = cartIds });

        //            if (rowsAffected > 0)
        //            {
        //                Console.WriteLine("Cart güncellendi");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Cart güncellenemedi veya hiçbir satır etkilenmedi.");
        //            }
        //        }
        //    }
        //}



    }


}

