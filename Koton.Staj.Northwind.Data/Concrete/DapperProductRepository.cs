
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Data.Queries;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

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

        public ResponseModel<List<Product>> GetAllProducts()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = ProductQueries.GET_ALL_PRODUCTS_QUERY;
                    var products = connection.Query<Product>(sql).ToList();

                    return new ResponseModel<List<Product>> { Success = true, Message = DataMessages.PRODUCT_GET_SUCCESS, Data = products };

                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<Product>> { Success = false, Message = DataMessages.PRODUCT_GET_ERROR, Data = null };

            }
        }

        public ResponseModel<List<Product>> GetAllProductsOrderByDescendingPrice()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = ProductQueries.GET_ALL_PRODUCTS_ORDER_BY_DESCENDING_PRICE_QUERY;
                    var products = connection.Query<Product>(sql).ToList();

                    return new ResponseModel<List<Product>> { Success = true, Message = DataMessages.PRODUCT_SORT_SUCCESS_DESC, Data = products };

                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<Product>> { Success = false, Message = DataMessages.PRODUCT_SORT_ERROR, Data = null };

            }
        }

        public ResponseModel<List<Product>> GetAllProductsOrderByAscendingPrice()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = ProductQueries.GET_ALL_PRODUCTS_ORDER_BY_ASCENDING_PRICE_QUERY;
                    var products = connection.Query<Product>(sql).ToList();

                    return new ResponseModel<List<Product>> { Success = true, Message = DataMessages.PRODUCT_SORT_SUCCESS_ASC, Data = products };

                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<Product>> { Success = false, Message = DataMessages.PRODUCT_SORT_ERROR, Data = null };

            }
        }
    }
}
