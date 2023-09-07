
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Queries;

namespace Koton.Staj.Northwind.Data.Concrete
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:SqlServerDb"];
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = ProductQueries.GET_ALL_PRODUCTS_QUERY;
                return connection.Query<Product>(sql);
            }
        }

        public IEnumerable<Product> GetAllProductsOrderByDescendingPrice()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = ProductQueries.GET_ALL_PRODUCTS_ORDER_BY_DESCENDING_PRICE_QUERY;
                return connection.Query<Product>(sql);
            }
        }

        public IEnumerable<Product> GetAllProductsOrderByAscendingPrice()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = ProductQueries.GET_ALL_PRODUCTS_ORDER_BY_ASCENDING_PRICE_QUERY;
                return connection.Query<Product>(sql);
            }
        }
        public Product GetProductById(int productId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = Queries.ProductQueries.GET_PRODUCT_BY_ID_QUERY;
                return connection.QuerySingleOrDefault<Product>(sql, new { ProductId = productId });
            }
        }
    }
}
