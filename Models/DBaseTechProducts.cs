using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechProduct.Models
{
    public class DBaseTechProducts:DbContext
    {
        public DBaseTechProducts()
        {
        }

        public DBaseTechProducts(DbContextOptions<DBaseTechProducts> options) : base(options)
        {

        }

        public DbSet<Product> dbProducts { get; set; }
        public DbSet<User> dbUsers { get; set; }
        public DbSet<ShippingDetail> dbOrders { get; set; }
        public DbSet<CarteLine> CarteLine { get; set; }
    }
}
