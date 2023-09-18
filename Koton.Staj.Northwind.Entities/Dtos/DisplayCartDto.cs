

namespace Koton.Staj.Northwind.Entities.Dtos
{
    public class DisplayCartDto
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal TotalCartAmount => Quantity * UnitPrice; // Hesaplama burada yapılıyor
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    //    public object Categories { get; set; }
    //    public object Product { get; set; }
    
    }
}
