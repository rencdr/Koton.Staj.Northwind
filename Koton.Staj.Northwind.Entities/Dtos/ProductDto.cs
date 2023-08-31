using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Entities.Dtos
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
