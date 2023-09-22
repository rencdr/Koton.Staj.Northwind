using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using System.Data.SqlClient;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Data.Queries;
using System.Data;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

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


        public ResponseModel<bool> AddToCart(Cart cartItem)
        {
            var response = new ResponseModel<bool>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.ADD_TO_CART_QUERY;
                connection.Execute(query, cartItem);
                response.Success = true;
            }

            return response;
        }




        public ResponseModel<bool> CheckUserExists(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.CHECK_USER_EXIST_QUERY;
                int count = connection.ExecuteScalar<int>(query, new { UserId = userId });

                return count > 0
                    ? new ResponseModel<bool> { Success = true, Message = DataMessages.USER_EXISTS, Data = true }
                    : new ResponseModel<bool> { Success = false, Message = DataMessages.USER_NOT_FOUND, Data = false };
            }
        }


        public ResponseModel<List<Cart>> GetCartItems(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.GET_CART_ITEMS_QUERY;
                var cartItems = connection.Query<Cart>(query, new { UserId = userId }).ToList();
                return new ResponseModel<List<Cart>> { Success = true, Message = DataMessages.CART_ITEMS_FETCH_SUCCESS, Data = cartItems };
            }
        }



        public ResponseModel<bool> RemoveFromCart(int userId, int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.REMOVE_FROM_CART_QUERY;
                connection.Execute(query, new { UserId = userId, ProductId = productId });
                return new ResponseModel<bool> { Success = true, Message = DataMessages.CART_REMOVE_SUCCESS, Data = true };
            }
        }



        public ResponseModel<bool> DeleteCartByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = CartQueries.DELETE_CART_QUERY;
                connection.Execute(query, new { UserId = userId });
                return new ResponseModel<bool> { Success = true, Message = DataMessages.CART_DELETE_SUCCESS, Data = true };
            }
        }


        public ResponseModel<Cart> GetCartByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = CartQueries.GET_CART_BY_USERID_QUERY;
            var cart = connection.QueryFirstOrDefault<Cart>(query, new { UserId = userId });

            return cart != null
                ? new ResponseModel<Cart> { Success = true, Message = DataMessages.CART_GET_SUCCESS, Data = cart }
                : new ResponseModel<Cart> { Success = false, Message = DataMessages.CART_NOT_FOUND, Data = null };
        }



        public async Task<ResponseModel<List<Cart>>> GetCartsByUserIdAsync(int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string query = CartQueries.GET_CART_BY_USERID_QUERY;
                var carts = await dbConnection.QueryAsync<Cart>(query, new { UserId = userId });

                return new ResponseModel<List<Cart>>
                {
                    Success = true,
                    Message = DataMessages.CART_ITEMS_FETCH_SUCCESS,
                    Data = carts.ToList()
                };
            }
        }





        public async Task<ResponseModel<bool>> UpdateCartAsync(int cartId)
        {
            var response = new ResponseModel<bool>();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string updateQuery = CartQueries.UPDATE_CART_QUERY;
                int rowsAffected = await dbConnection.ExecuteAsync(updateQuery, new { CartId = cartId });

                if (rowsAffected > 0)
                {
                    response.Success = true;
                    response.Message = DataMessages.CART_UPDATE_SUCCESS;
                    response.Data = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = DataMessages.CART_UPDATE_ERROR;
                    response.Data = false;
                }
            }

            return response;
        }



        public async Task<ResponseModel<bool>> UpdateCartByOrderIdAsync(int orderId)
        {
            var response = new ResponseModel<bool>();


            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string cartIdsQuery = CartQueries.CARTS_IDS_QUERY;

                IEnumerable<int> cartIds = await dbConnection.QueryAsync<int>(cartIdsQuery, new { OrderId = orderId });

                if (!cartIds.Any())
                {
                    response.Success = false;
                    response.Message = "Cart bulunamadı.";
                    response.Data = false;
                }
                else
                {
                    string updateQuery = CartQueries.UPDATE_QUERY;

                    int rowsAffected = await dbConnection.ExecuteAsync(updateQuery, new { CartIds = cartIds });

                    if (rowsAffected <= 0)
                    {
                        response.Success = false;
                        response.Message = DataMessages.CART_UPDATE_ERROR;
                        response.Data = false;
                    }
                    else
                    {
                        response.Success = true;
                        response.Message = DataMessages.CART_UPDATE_SUCCESS;
                        response.Data = true;
                    }
                }
            }

            return response;
        }




    }


}

