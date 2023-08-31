

namespace Koton.Staj.Northwind.Entities
{
    public class Product
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
