using AutoMapper;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;
using System.Collections.Generic;

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

        public ResponseModel<List<ProductDto>> GetAllProducts()
        {
            try
            {
                var products = _productRepository.GetAllProducts();
                var productDtos = _mapper.Map<List<ProductDto>>(products);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = true,
                    Message = "Ürünler başarıyla alındı.",
                    Data = productDtos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = false,
                    Message = "Ürünler alınırken bir hata oluştu.",
                    Data = null
                };
            }
        }


        //public List<ProductDto> GetAllProducts()
        //{
        //    var products = _productRepository.GetAllProducts();
        //    var productDtos = _mapper.Map<List<ProductDto>>(products);
        //    return productDtos;
        //}

        public ResponseModel<List<ProductDto>> GetAllProductsOrderByDescendingPrice()
        {
            try
            {
                var products = _productRepository.GetAllProductsOrderByDescendingPrice();
                var productDtos = _mapper.Map<List<ProductDto>>(products);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = true,
                    Message = "Ürünler başarıyla alındı ve fiyata göre azalan sırada sıralandı.",
                    Data = productDtos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = false,
                    Message = "Ürünler alınırken veya sıralanırken bir hata oluştu.",
                    Data = null
                };
            }
        }


        //public List<ProductDto> GetAllProductsOrderByDescendingPrice()
        //{
        //    var products = _productRepository.GetAllProductsOrderByDescendingPrice();
        //    var productDtos = _mapper.Map<List<ProductDto>>(products);
        //    return productDtos;
        //}



        public ResponseModel<List<ProductDto>> GetAllProductsOrderByAscendingPrice()
        {
            try
            {
                var products = _productRepository.GetAllProductsOrderByAscendingPrice();
                var productDtos = _mapper.Map<List<ProductDto>>(products);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = true,
                    Message = "Ürünler başarıyla alındı ve fiyata göre artan sırada sıralandı.",
                    Data = productDtos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);

                return new ResponseModel<List<ProductDto>>
                {
                    Success = false,
                    Message = "Ürünler alınırken veya sıralanırken bir hata oluştu.",
                    Data = null
                };
            }
        }

        //public List<ProductDto> GetAllProductsOrderByAscendingPrice()
        //{
        //    var products = _productRepository.GetAllProductsOrderByAscendingPrice();
        //    var productDtos = _mapper.Map<List<ProductDto>>(products);
        //    return productDtos;
        //}
    }
}



