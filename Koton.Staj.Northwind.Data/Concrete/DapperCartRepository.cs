
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using System.Data.SqlClient;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Data.Queries;

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
                var query = CartQueries.ADD_TO_CART_QUERY;
                connection.Execute(query, cartItem);

            }
        }
        public List<Cart> GetCartItems(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.GET_CART_ITEMS_QUERY;

                return connection.Query<Cart>(query, new { UserId = userId }).ToList();
            }


        }

        public void RemoveFromCart(int userId, int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.REMOVE_FROM_CART_QUERY;
                connection.Execute(query, new { UserId = userId, ProductId = productId });
            }
        }



        public void DeleteCartByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.DELETE_CART_QUERY;
                connection.Execute(query, new { UserId = userId });
            }
        }


        public Cart GetCartByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = CartQueries.GET_CART_BY_USERID_QUERY;
            return connection.QueryFirstOrDefault<Cart>(query, new { UserId = userId });
        }

        public IEnumerable<Cart> GetCartsByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.GET_CART_BY_USERID_QUERY;
                return connection.Query<Cart>(query, new { UserId = userId });
            }
        }


    }
}
