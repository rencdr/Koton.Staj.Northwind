
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetAllProductsOrderByDescendingPrice();

        IEnumerable<Product> GetAllProductsOrderByAscendingPrice();

        Product GetProductById(int productId);


    }
}
