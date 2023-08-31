
using Koton.Staj.Northwind.Entities;    

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProductsOrderByDescendingPrice();
        IEnumerable<Product> GetAllProductsOrderByAscendingPrice();

    }
}
