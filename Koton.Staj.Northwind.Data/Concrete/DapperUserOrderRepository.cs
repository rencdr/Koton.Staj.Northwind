
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Koton.Staj.Northwind.Data.Abstract;
using Microsoft.Extensions.Configuration;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Data.Concrete
{
    public class DapperUserOrderRepository : IUserOrderRepository
    {
        private readonly string _connectionString;

        public DapperUserOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
        }


        public async Task<ResponseModel<UserOrder>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string query = Queries.OrderQueries.GET_ORDER_BY_ID_QUERY;
                    var order = await dbConnection.QueryFirstOrDefaultAsync<UserOrder>(query, new { OrderId = orderId });

                    return order != null
                        ? new ResponseModel<UserOrder> { Success = true, Message = DataMessages.ORDER_RETRIEVED_SUCCESS, Data = order }
                        : new ResponseModel<UserOrder> { Success = false, Message = DataMessages.ORDER_NOT_FOUND, Data = null };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserOrder> { Success = false, Message = DataMessages.ORDER_RETRIEVAL_ERROR + ": " + ex.Message, Data = null };
            }
        }

     

        public async Task<ResponseModel<bool>> InsertUserOrderAsync(UserOrder order)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string insertQuery = Queries.OrderQueries.INSERT_USER_ORDER_QUERY;
                    int rowsAffected = await dbConnection.ExecuteAsync(insertQuery, order);

                    return new ResponseModel<bool> { Success = true, Message = DataMessages.CART_ADD_SUCCESS, Data = true };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = DataMessages.CART_ADD_ERROR, Data = false };
            }
        }

        public async Task<ResponseModel<bool>> CancelUserOrderAsync(int orderId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string deleteQuery = Queries.OrderQueries.CANCEL_USER_ORDER_QUERY;
                    int rowsAffected = await dbConnection.ExecuteAsync(deleteQuery, new { OrderId = orderId });

                    return new ResponseModel<bool> { Success = rowsAffected > 0, Message = rowsAffected > 0 ? DataMessages.ORDER_CANCEL_SUCCESS : DataMessages.ORDER_CANCEL_ERROR, Data = rowsAffected > 0 };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = DataMessages.ORDER_CANCEL_ERROR, Data = false };
            }
        }


        public async Task<ResponseModel<int>> GetLastInsertedOrderIdAsync(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    await ((SqlConnection)dbConnection).OpenAsync();
                    string query = Queries.OrderQueries.GET_LAST_ORDERID_QUERY;
                    int lastOrderId = await dbConnection.QueryFirstOrDefaultAsync<int>(query, new { userId });
                    return new ResponseModel<int> { Success = true, Message = DataMessages.ORDER_RETRIEVED_SUCCESS, Data = lastOrderId };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Success = false, Message = DataMessages.ORDER_RETRIEVAL_ERROR + ": " + ex.Message, Data = 0 };
            }
        }

        public async Task<ResponseModel<List<UserOrder>>> GetOrdersByUserIdAsync(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string query = Queries.OrderQueries.GET_ORDERS_BY_USER_ID_QUERY;
                    var orders = await dbConnection.QueryAsync<UserOrder>(query, new { UserId = userId });

                    return orders != null && orders.Any()
                        ? new ResponseModel<List<UserOrder>> { Success = true, Message = DataMessages.ORDERS_RETRIEVED_SUCCESS, Data = orders.ToList() }
                        : new ResponseModel<List<UserOrder>> { Success = false, Message = DataMessages.ORDERS_NOT_FOUND, Data = null };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<UserOrder>> { Success = false, Message = DataMessages.ORDERS_RETRIEVAL_ERROR + ": " + ex.Message, Data = null };
            }
        }


       
    }
}
