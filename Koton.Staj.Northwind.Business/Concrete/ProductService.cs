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
                var response = _productRepository.GetAllProducts(); 
                if (response.Success)
                {
                    var products = response.Data;
                    var productDtos = _mapper.Map<List<ProductDto>>(products);

                    return new ResponseModel<List<ProductDto>> { Success = true, Message = Messages.PRODUCTS_RETRIEVED_SUCCESS, Data = productDtos };
                }
                else
                {
                    return new ResponseModel<List<ProductDto>> { Success = false, Message = response.Message, Data = null };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<ProductDto>> { Success = false, Message = Messages.PRODUCTS_RETRIEVAL_ERROR + ": " + ex.Message, Data = null };
            }
        }



        public ResponseModel<List<ProductDto>> GetAllProductsOrderByDescendingPrice()
        {
            try
            {
                var response = _productRepository.GetAllProductsOrderByDescendingPrice(); 
                if (response.Success)
                {
                    var products = response.Data;
                    var productDtos = _mapper.Map<List<ProductDto>>(products);

                    return new ResponseModel<List<ProductDto>> { Success = true, Message = Messages.PRODUCTS_SORTED_BY_PRICE_DESC, Data = productDtos };
                }
                else
                {
                    return new ResponseModel<List<ProductDto>> { Success = false, Message = response.Message, Data = null };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<ProductDto>> { Success = false, Message = Messages.PRODUCTS_SORT_ERROR + ": " + ex.Message, Data = null };
            }
        }



        public ResponseModel<List<ProductDto>> GetAllProductsOrderByAscendingPrice()
        {
            try
            {
                var response = _productRepository.GetAllProductsOrderByAscendingPrice(); 
                if (response.Success)
                {
                    var products = response.Data;
                    var productDtos = _mapper.Map<List<ProductDto>>(products);

                    return new ResponseModel<List<ProductDto>> { Success = true, Message = Messages.PRODUCTS_SORTED_BY_PRICE_ASC, Data = productDtos };
                }
                else
                {
                    return new ResponseModel<List<ProductDto>> { Success = false, Message = response.Message, Data = null };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<List<ProductDto>> { Success = false, Message = Messages.PRODUCTS_SORT_ERROR + ": " + ex.Message, Data = null };
            }
        }

        

    }
}



