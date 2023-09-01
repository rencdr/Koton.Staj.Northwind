
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;

namespace Koton.Staj.Data.Abstract
{
    public interface ICartRepository
    {
        void AddToCart(Cart cartItem);
        void RemoveFromCart(int userId, int productId);
        List<Cart> GetCartItems(int userId);

        void DeleteCartByUserId(int userId);

        Cart GetCartByUserId(int userId);
        IEnumerable<Cart> GetCartsByUserId(int userId);


    }
}
