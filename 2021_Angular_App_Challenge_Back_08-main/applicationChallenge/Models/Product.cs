using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int AmountInStock { get; set; }
        public string ImageName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int AmountInPackage { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
