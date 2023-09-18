using FluentValidation;
using AutoMapper;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Business.Validation;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Koton.Staj.Northwind.Entities.Concrete;
using Koton.Staj.Northwind.Business.Mapper;
using System.Data.SqlClient;

namespace Koton.Staj.Northwind.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly string _connectionString;
        private readonly AddToCartDtoValidator _addToCartValidator;
        private readonly IMapper _mapper;



        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IConfiguration configuration, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
            _addToCartValidator = new AddToCartDtoValidator();
            _mapper = mapper;           

        }
       
        public ResponseModel<List<string>> AddToCart(AddToCartDto cartItem)
        {
            var validationResult = _addToCartValidator.Validate(cartItem);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ResponseModel<List<string>> { Success = false, Message = Messages.INVALID_INPUTS, Data = errors };

            }


            var userCheckResponse = _cartRepository.CheckUserExists(cartItem.UserId);

            if (!userCheckResponse.Success)
            {
                return new ResponseModel<List<string>> { Success = false, Message = userCheckResponse.Message, Data = null };

            }
            var cart = new Cart
            {
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };

            _cartRepository.AddToCart(cart);

            return new ResponseModel<List<string>> { Success = true, Message = Messages.PRODUCT_ADDED_TO_CART, Data = null };

        }

        public ResponseModel<List<DisplayCartDto>> GetCartItems(int userId)
        {
            try
            {
                var responseModel = _cartRepository.GetCartItems(userId);
                var displayCartDtos = _mapper.Map<List<DisplayCartDto>>(responseModel.Data);

                return new ResponseModel<List<DisplayCartDto>> { Success = responseModel.Success, Message = responseModel.Message, Data = displayCartDtos };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<List<DisplayCartDto>> { Success = false, Message = "Sepet öğeleri alınırken bir hata oluştu.", Data = null };
            }
        }



        public ResponseModel<bool> DeleteCartByUserId(int userId)
        {
            try
            {
                _cartRepository.DeleteCartByUserId(userId);
                return new ResponseModel<bool> { Success = true, Message = Messages.CART_DELETED_SUCCESS, Data = true };

            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.CART_DELETION_ERROR + ": " + ex.Message, Data = false };

            }
        }



        public async Task<ResponseModel<List<Cart>>> GetCartsByUserIdAsync(int userId)
        {


            try
            {
                IEnumerable<Cart> carts = (IEnumerable<Cart>)await _cartRepository.GetCartsByUserIdAsync(userId);
                return new ResponseModel<List<Cart>> { Success = true, Message = Messages.CARTS_RETRIEVED_SUCCESS, Data = carts.ToList() };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<Cart>> { Success = false, Message = Messages.CARTS_RETRIEVAL_ERROR + ": " + ex.Message, Data = null };
            }
        }

            

        public ResponseModel<bool> RemoveFromCart(int userId, int productId)
        {
            try
            {
                _cartRepository.RemoveFromCart(userId, productId);
                return new ResponseModel<bool> { Success = true, Message = Messages.PRODUCT_REMOVED_FROM_CART_SUCCESS, Data = true };
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Success = false, Message = Messages.PRODUCT_REMOVAL_FROM_CART_ERROR + ": " + ex.Message, Data = false };
            }
        }

       


    }
}