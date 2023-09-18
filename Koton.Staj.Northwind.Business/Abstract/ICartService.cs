
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities.Concrete;
using Koton.Staj.Northwind.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface ICartService
    {
        ResponseModel<List<string>> AddToCart(AddToCartDto cartItem);

        ResponseModel<bool> RemoveFromCart(int userId, int productId);

        ResponseModel<List<DisplayCartDto>> GetCartItems(int userId);

        ResponseModel<bool> DeleteCartByUserId(int userId);

        Task<ResponseModel<List<Cart>>> GetCartsByUserIdAsync(int userId);


    }



}
