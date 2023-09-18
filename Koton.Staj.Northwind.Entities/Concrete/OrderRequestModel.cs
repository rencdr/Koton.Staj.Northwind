using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.Staj.Northwind.Entities.Concrete
{
    public class OrderRequestModel
    {
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        //public int OrderId { get; set; }

    }
}
