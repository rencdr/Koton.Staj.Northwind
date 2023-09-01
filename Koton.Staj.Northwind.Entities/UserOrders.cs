using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Entities
{
    public class UserOrders
    {
        public int UserOrderId { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public string ProductName { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
