using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class ShippingDetail
    {
        [Key]
        public int id { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Enter your Full Name")]
        public string FullName { get; set; }   
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Adress")]
        public string Adress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your City")]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Gouvernerat")]
        public string Gouvernerat { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Zip")]
        public string Zip { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Phone Number")]
        public string Phone { get; set; }
        public List<CarteLine> orders { get; set; }
        public User buyer { get; set; }
        public DateTime orderDate { get; set; }
        public bool state { get; set; }

    }
}
