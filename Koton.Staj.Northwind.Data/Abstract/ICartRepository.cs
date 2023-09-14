
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Data.Abstract;

namespace Koton.Staj.Data.Abstract
{
    public interface ICartRepository
    {
        bool AddToCart(Cart cartItem);

        bool RemoveFromCart(int userId, int productId);

        List<Cart> GetCartItems(int userId);

        bool DeleteCartByUserId(int userId);

        Cart GetCartByUserId(int userId);

        List<Cart> GetCartsByUserId(int userId);

        bool UpdateCart(int cartId);

        bool UpdateCartByOrderId(int orderId);


    }
}
