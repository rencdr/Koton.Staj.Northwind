using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Data.Abstract
{
    public interface ICartRepository
    {
        ResponseModel<bool> AddToCart(Cart cartItem);

        ResponseModel<bool> CheckUserExists(int userId);

        ResponseModel<bool> RemoveFromCart(int userId, int productId);

        ResponseModel<List<Cart>> GetCartItems(int userId);

        ResponseModel<bool> DeleteCartByUserId(int userId);

        ResponseModel<Cart> GetCartByUserId(int userId);

        Task<ResponseModel<List<Cart>>> GetCartsByUserIdAsync(int userId);

        Task<ResponseModel<bool>> UpdateCartAsync(int cartId);

        Task<ResponseModel<bool>> UpdateCartByOrderIdAsync(int orderId);
    }
}
