﻿namespace Koton.Staj.Northwind.Entities
{
    public class Product
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Categories Categories { get; set; } // Category nesnesi ile ilişkilendirildi


    }
}

