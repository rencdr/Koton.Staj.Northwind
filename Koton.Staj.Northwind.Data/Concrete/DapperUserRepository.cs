
using System.Data.SqlClient;
using System.Data;
using Koton.Staj.Northwind.Data.Abstract;
using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Queries;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Data.Concrete
{
    public class DapperUserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public DapperUserRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = _configuration["ConnectionStrings:SqlServerDb"];
        }


        public async Task<ResponseModel<int>> CreateUserAsync(User user)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = UserQueries.CREATE_USE_REGISTER;
                    int userId = await connection.ExecuteScalarAsync<int>(sql, user);

                    return userId > 0
                        ? new ResponseModel<int> { Success = true, Message = DataMessages.USER_CREATED_SUCCESS, Data = userId }
                        : new ResponseModel<int> { Success = false, Message = DataMessages.USER_CREATION_ERROR, Data = 0 };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<int> { Success = false, Message = DataMessages.USER_CREATION_ERROR + ": " + ex.Message, Data = 0 };
            }
        }

        public async Task<ResponseModel<User>> GetUserByUsernameAsync(string username)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = UserQueries.AUTH_USER;
                    var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });

                    return user != null
                        ? new ResponseModel<User> { Success = true, Message = DataMessages.USER_AUTH_SUCCESS, Data = user }
                        : new ResponseModel<User> { Success = false, Message = DataMessages.USER_NOT_FOUND, Data = null };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<User> { Success = false, Message = DataMessages.USER_AUTH_ERROR + ": " + ex.Message, Data = null };

            }
        }




    }
}
