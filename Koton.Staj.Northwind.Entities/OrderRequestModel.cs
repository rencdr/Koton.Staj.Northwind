using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Entities
{
    public class OrderRequestModel
    {
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
