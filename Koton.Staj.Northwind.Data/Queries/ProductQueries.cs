

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class ProductQueries
    {

       
        internal const string GET_ALL_PRODUCTS_QUERY = @"
    SELECT
        p.ProductName,
        p.UnitsInStock,
        p.UnitPrice,
        c.CategoryName,
        c.Description
    FROM
        Products p
    JOIN
        Categories c ON p.CategoryID = c.CategoryID;
";

        internal const string GET_ALL_PRODUCTS_ORDER_BY_DESCENDING_PRICE_QUERY = @"
    SELECT
        p.ProductName,
        p.UnitsInStock,
        p.UnitPrice,
        c.CategoryName,
        c.Description
    FROM
        Products p
    JOIN
        Categories c ON p.CategoryID = c.CategoryID
     ORDER BY UnitPrice DESC

";
        internal const string GET_ALL_PRODUCTS_ORDER_BY_ASCENDING_PRICE_QUERY = @"
    SELECT
        p.ProductName,
        p.UnitsInStock,
        p.UnitPrice,
        c.CategoryName,
        c.Description
    FROM
        Products p
    JOIN
        Categories c ON p.CategoryID = c.CategoryID
     ORDER BY UnitPrice ASC

";


    }
}
