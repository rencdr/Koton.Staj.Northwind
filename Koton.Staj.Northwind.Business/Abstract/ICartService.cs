using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface ICartService
    {
        void AddToCart(Cart cartItem);
        void RemoveFromCart(int userId, int productId);
        List<Cart> GetCartItems(int userId);
    }
}
