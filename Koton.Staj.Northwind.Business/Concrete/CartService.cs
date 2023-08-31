using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
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

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _connectionString = configuration["ConnectionStrings:SqlServerDb"];
        }
        //private readonly ICartRepository _cartRepository;

        //public CartService(ICartRepository cartRepository)
        //{
        //    _cartRepository = cartRepository;
        //}
        public void AddToCart(AddToCartDto cartItem)
        {
            var cart = new Cart
            {
                UserId = cartItem.UserId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
            _cartRepository.AddToCart(cart);
        }
        //public void AddToCart(Cart cartItem)
        //{
        //    _cartRepository.AddToCart(cartItem);
        //}
        public List<DisplayCartDto> GetCartItems(int userId)
        {
            var carts = _cartRepository.GetCartItems(userId);
            var displayCartDtos = new List<DisplayCartDto>();

            foreach (var cart in carts)
            {
                var product = _productRepository.GetProductById(cart.ProductId);


                var displayCartDto = new DisplayCartDto
                {
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    CategoryName = cart.CategoryName, // Product sınıfından CategoryName 
                    Description = cart.Description,
                    TotalCartAmount = cart.TotalCartAmount
                };

                displayCartDtos.Add(displayCartDto);
            }

            return displayCartDtos;
        }
        //public List<Cart> GetCartItems(int userId)
        //{
        //    return _cartRepository.GetCartItems(userId);
        //}

        public void RemoveFromCart(int userId, int productId)
        {
            _cartRepository.RemoveFromCart(userId, productId);
        }
    }
}
