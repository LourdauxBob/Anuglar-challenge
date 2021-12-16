using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Models
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public int Amount { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
