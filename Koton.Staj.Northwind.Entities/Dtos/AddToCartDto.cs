
using System.ComponentModel.DataAnnotations;

namespace Koton.Staj.Northwind.Entities.Dtos
{
    public class AddToCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
