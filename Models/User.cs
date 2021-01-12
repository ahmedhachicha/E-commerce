using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class User
    {
        [Key]
        public long id { get; set; }
        public String FullName { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String role { get; set; }

    }
}
