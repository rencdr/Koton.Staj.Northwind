﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Entities.Dtos
{
    public class DisplayCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal TotalCartAmount { get; set; }

    }
}
