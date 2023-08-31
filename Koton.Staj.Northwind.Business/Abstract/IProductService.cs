
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IProductService
    {
        //IEnumerable<Product> GetAllProducts();
        //IEnumerable<Product> GetAllProductsOrderByDescendingPrice();
        //IEnumerable<Product> GetAllProductsOrderByAscendingPrice();
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<ProductDto> GetAllProductsOrderByDescendingPrice();
        IEnumerable<ProductDto> GetAllProductsOrderByAscendingPrice();

    }
}
