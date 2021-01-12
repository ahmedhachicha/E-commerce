using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class CarteLine2
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
