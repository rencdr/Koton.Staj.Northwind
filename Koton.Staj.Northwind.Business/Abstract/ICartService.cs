
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface ICartService
    {
        CartOperationResult AddToCart(AddToCartDto cartItem);

        ResponseModel RemoveFromCart(int userId, int productId); 
        List<DisplayCartDto> GetCartItems(int userId);
        ResponseModel DeleteCartByUserId(int userId); 
        IEnumerable<Cart> GetCartsByUserId(int userId);


    }

    public class CartOperationResult
    {
        public int CartId { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }

    }

}
