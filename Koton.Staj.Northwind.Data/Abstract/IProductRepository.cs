
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;
using System.Collections.Generic;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IProductRepository
    {
        ResponseModel<List<Product>> GetAllProducts();
        ResponseModel<List<Product>> GetAllProductsOrderByDescendingPrice();
        ResponseModel<List<Product>> GetAllProductsOrderByAscendingPrice();
    }
}
