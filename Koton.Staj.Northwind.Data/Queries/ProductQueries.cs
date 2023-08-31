

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class ProductQueries
    {

        internal const string GET_ALL_PRODUCTS_QUERY = "SELECT * FROM Products";

        internal const string GET_ALL_PRODUCTS_ORDER_BY_DESCENDING_PRICE_QUERY = "SELECT * FROM Products ORDER BY UnitPrice DESC";

        internal const string GET_ALL_PRODUCTS_ORDER_BY_ASCENDING_PRICE_QUERY = "SELECT * FROM Products ORDER BY UnitPrice ASC";


    }
}
