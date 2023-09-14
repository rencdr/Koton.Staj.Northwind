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
                return new ResponseModel<List<string>>
                {
                    Success = false,
                    Message = "Geçersiz girişler bulundu.",
                    Data = errors
                };
            }

            var cart = new Cart
            {
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };

            _cartRepository.AddToCart(cart);

            return new ResponseModel<List<string>>
            {
                Success = true,
                Message = "Ürün başarıyla sepete eklendi.",
                Data = null
            };
        }


        //public List<string> AddToCart(AddToCartDto cartItem)
        //{
        //    var validationResult = _addToCartValidator.Validate(cartItem);
        //    var errors = new List<string>();

        //    if (!validationResult.IsValid)
        //    {
        //        errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        //        return errors;
        //    }

        //    var cart = new Cart
        //    {
        //        UserId = cartItem.UserId,
        //        ProductId = cartItem.ProductId,
        //        Quantity = cartItem.Quantity
        //    };

        //    _cartRepository.AddToCart(cart);

        //    return errors; 
        //}


        public ResponseModel<bool> DeleteCartByUserId(int userId)
        {
            try
            {
                _cartRepository.DeleteCartByUserId(userId);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Sepet başarıyla silindi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Sepet silinirken bir hata oluştu.",
                    Data = false
                };
            }
        }


        //public bool DeleteCartByUserId(int userId)
        //{
        //    try
        //    {
        //        _cartRepository.DeleteCartByUserId(userId);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Hata Mesajı: " + ex.Message);
        //        return false; 
        //    }
        //}


        //public List<DisplayCartDto> GetCartItems(int userId)
        //{
        //    var carts = _cartRepository.GetCartItems(userId);
        //    var displayCartDtos = _mapper.Map<List<DisplayCartDto>>(carts);
        //    return displayCartDtos;




        public ResponseModel<List<DisplayCartDto>> GetCartItems(int userId)
        {
            try
            {
                var carts = _cartRepository.GetCartItems(userId);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Cart, DisplayCartDto>();
                });

                IMapper mapper = config.CreateMapper();

                var displayCartDtos = mapper.Map<List<DisplayCartDto>>(carts);

                return new ResponseModel<List<DisplayCartDto>>
                {
                    Success = true,
                    Message = "Sepet öğeleri başarıyla alındı.",
                    Data = displayCartDtos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<List<DisplayCartDto>>
                {
                    Success = false,
                    Message = "Sepet öğeleri alınırken bir hata oluştu.",
                    Data = null
                };
            }
        }


        //DI da tanımla ama categories tablosundan bilgi alamıyosun unutma!!
        //public List<DisplayCartDto> GetCartItems(int userId)
        //{
        //    var carts = _cartRepository.GetCartItems(userId);

        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Cart, DisplayCartDto>();
        //    });

        //    IMapper mapper = config.CreateMapper();

        //    var displayCartDtos = mapper.Map<List<DisplayCartDto>>(carts);

        //    return displayCartDtos;
        //}


        public ResponseModel<List<Cart>> GetCartsByUserId(int userId)
        {
            try
            {
                IEnumerable<Cart> carts = _cartRepository.GetCartsByUserId(userId);
                return new ResponseModel<List<Cart>>
                {
                    Success = true,
                    Message = "Sepetler başarıyla alındı.",
                    Data = carts.ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<List<Cart>>
                {
                    Success = false,
                    Message = "Sepetler alınırken bir hata oluştu.",
                    Data = null
                };
            }
        }

        //public List<Cart> GetCartsByUserId(int userId)
        //{
        //    IEnumerable<Cart> carts = _cartRepository.GetCartsByUserId(userId);
        //    return carts.ToList();
        //}


        public ResponseModel<bool> RemoveFromCart(int userId, int productId)
        {
            try
            {
                _cartRepository.RemoveFromCart(userId, productId);
                return new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Ürün sepetten başarıyla kaldırıldı.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Mesajı: " + ex.Message);
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Ürün sepetten kaldırılırken bir hata oluştu.",
                    Data = false
                };
            }
        }

        //public bool RemoveFromCart(int userId, int productId)
        //{
        //    try
        //    {
        //        _cartRepository.RemoveFromCart(userId, productId);
        //        return true; 
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Hata Mesajı: " + ex.Message);
        //        return false; 
        //    }
        //}


    }
}