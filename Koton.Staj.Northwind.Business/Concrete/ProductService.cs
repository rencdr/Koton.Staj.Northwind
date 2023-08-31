
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Business.Utilities;


namespace Koton.Staj.Northwind.Business.Concrete
{
    //kullanıcı erişimi yok response modele gerek var mı?
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAllProducts()
        {

            return _productRepository.GetAllProducts();
        }
        //
        public IEnumerable<Product> GetAllProductsOrderByDescendingPrice()
        {
            return _productRepository.GetAllProductsOrderByDescendingPrice();
        }
        public IEnumerable<Product> GetAllProductsOrderByAscendingPrice()
        {
            return _productRepository.GetAllProductsOrderByAscendingPrice();
        }


    }
}
