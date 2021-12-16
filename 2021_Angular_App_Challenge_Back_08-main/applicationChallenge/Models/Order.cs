using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime? DatePlaced { get; set; }
        public DateTime? DateCompleted { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
