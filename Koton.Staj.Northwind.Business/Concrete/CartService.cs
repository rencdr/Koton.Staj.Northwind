using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Business.Validation;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Koton.Staj.Northwind.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly string _connectionString;
        private readonly AddToCartDtoValidator _addToCartValidator;



        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
            _addToCartValidator = new AddToCartDtoValidator(); 



        }

        public CartOperationResult AddToCart(AddToCartDto cartItem)
        {
            var validationResult = _addToCartValidator.Validate(cartItem);
            var result = new CartOperationResult();

            if (!validationResult.IsValid)
            {
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return result;
            }

            var cart = new Cart
            {
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };

            _cartRepository.AddToCart(cart);
            result.Success = true;
            result.CartId = cart.Id;

            return result;
        }



       


        public ResponseModel DeleteCartByUserId(int userId)
        {
            try
            {
                _cartRepository.DeleteCartByUserId(userId);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Cart deleted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while deleting the cart: " + ex.Message,
                    Data = null
                };
            }
        }

      

        public List<DisplayCartDto> GetCartItems(int userId)
        {
            var carts = _cartRepository.GetCartItems(userId);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, DisplayCartDto>();
            });

            IMapper mapper = config.CreateMapper();

            var displayCartDtos = mapper.Map<List<DisplayCartDto>>(carts);

            return displayCartDtos;
        }
      

        public IEnumerable<Cart> GetCartsByUserId(int userId)
        {
            return _cartRepository.GetCartsByUserId(userId);
        }




        public ResponseModel RemoveFromCart(int userId, int productId)
        {
            try
            {
                _cartRepository.RemoveFromCart(userId, productId);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Product removed from cart successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "An error occurred while removing the product from the cart: " + ex.Message,
                    Data = null
                };
            }
        }

    }
}