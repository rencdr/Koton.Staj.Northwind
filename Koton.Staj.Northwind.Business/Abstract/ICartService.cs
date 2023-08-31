using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface ICartService
    {
        // void AddToCart(Cart cartItem);
        void AddToCart(AddToCartDto cartItem);
        void RemoveFromCart(int userId, int productId);
        List<DisplayCartDto> GetCartItems(int userId);

        // List<Cart> GetCartItems(int userId);
    }
}
