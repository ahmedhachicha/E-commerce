using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class CarteLine
    {
        [Key]
        public int Id { get; set; }  // must be public!
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int ShippingDetailid { get; set; }
    }
}
