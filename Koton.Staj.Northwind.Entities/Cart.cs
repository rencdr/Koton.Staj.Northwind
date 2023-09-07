

namespace Koton.Staj.Northwind.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime OrderedTime { get; set; }
        public DateTime? DeletedTime { get; set; } // DateTime? kullanarak DeletedTime'ı nullable yap
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public decimal TotalPrice { get; set; } // (Quantity * UnitPrice) yerine TotalPrice alanı
        public decimal TotalCartAmount { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }




    }
}
