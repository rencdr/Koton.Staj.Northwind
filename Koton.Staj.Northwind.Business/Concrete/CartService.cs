using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koton.Staj.Data.Abstract;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(Cart cartItem)
        {
            _cartRepository.AddToCart(cartItem);
        }

        public List<Cart> GetCartItems(int userId)
        {
            return _cartRepository.GetCartItems(userId);
        }

        public void RemoveFromCart(int userId, int productId)
        {
            _cartRepository.RemoveFromCart(userId, productId);
        }
    }
}
