
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IProductRepository
    {

        List<Product> GetAllProducts();
        List<Product> GetAllProductsOrderByDescendingPrice();
        List<Product> GetAllProductsOrderByAscendingPrice();
        Product GetProductById(int productId);


    }
}
