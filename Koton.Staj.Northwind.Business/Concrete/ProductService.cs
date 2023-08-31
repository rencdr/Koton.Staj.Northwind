
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Concrete
{
    
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                CategoryName = p.CategoryName,
                Description = p.Description
            });

            return productDtos;
        }

        public IEnumerable<ProductDto> GetAllProductsOrderByDescendingPrice()
        {
            var products = _productRepository.GetAllProductsOrderByDescendingPrice();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                CategoryName = p.CategoryName,
                Description = p.Description
            });

            return productDtos;
        }

        public IEnumerable<ProductDto> GetAllProductsOrderByAscendingPrice()
        {
            var products = _productRepository.GetAllProductsOrderByAscendingPrice();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                CategoryName = p.CategoryName,
                Description = p.Description
            });

            return productDtos;
        }



    }
}
