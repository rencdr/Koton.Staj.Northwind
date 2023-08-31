
using System.Data.SqlClient;
using System.Data;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using Koton.Staj.Northwind.Data.Queries;

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


        public async Task<int> CreateUserAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = UserQueries.CREATE_USE_REGISTER;
                return await connection.ExecuteScalarAsync<int>(sql, user);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = UserQueries.AUTH_USER;
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
            }
        }
    }
}
