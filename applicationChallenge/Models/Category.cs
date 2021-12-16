using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public bool IsFood { get; set; }
        public ICollection<Product> Products { get; set; }

        
    }
}
