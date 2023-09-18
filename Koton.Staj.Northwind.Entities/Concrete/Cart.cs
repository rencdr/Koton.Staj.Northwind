namespace Koton.Staj.Northwind.Entities.Concrete
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime OrderedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Product Product { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public Categories Categories { get; set; } 
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }




    }
}

