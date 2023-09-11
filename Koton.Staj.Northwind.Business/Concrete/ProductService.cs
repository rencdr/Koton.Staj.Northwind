
using AutoMapper;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Concrete
{
    
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }


        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }

       

        public IEnumerable<ProductDto> GetAllProductsOrderByDescendingPrice()
        {
            var products = _productRepository.GetAllProductsOrderByDescendingPrice();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }

       

        public IEnumerable<ProductDto> GetAllProductsOrderByAscendingPrice()
        {
            var products = _productRepository.GetAllProductsOrderByAscendingPrice();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }

       



    }
}
