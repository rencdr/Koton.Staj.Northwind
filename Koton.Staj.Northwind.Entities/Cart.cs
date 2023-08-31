

namespace Koton.Staj.Northwind.Entities
{
    public class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public string CategoryName { get; set; } 
        public string Description { get; set; } 
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; } // (Quantity * UnitPrice)
        public decimal TotalCartAmount { get; set; }



    }
}
