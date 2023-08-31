
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface ICartService
    {
        void AddToCart(AddToCartDto cartItem);
        void RemoveFromCart(int userId, int productId);
        List<DisplayCartDto> GetCartItems(int userId);

    }
}
