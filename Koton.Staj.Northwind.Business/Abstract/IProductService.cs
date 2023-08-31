
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IProductService
    {
      
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<ProductDto> GetAllProductsOrderByDescendingPrice();
        IEnumerable<ProductDto> GetAllProductsOrderByAscendingPrice();

    }
}
