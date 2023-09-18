using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IProductService
    {
        ResponseModel<List<ProductDto>> GetAllProducts();

        ResponseModel<List<ProductDto>> GetAllProductsOrderByDescendingPrice();

        ResponseModel<List<ProductDto>> GetAllProductsOrderByAscendingPrice();

    }
}


