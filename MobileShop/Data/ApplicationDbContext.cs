using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MobileShop.Models;

namespace MobileShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MobileShop.Models.Mobile> Mobile { get; set; }
        public DbSet<MobileShop.Models.Seller> Seller { get; set; }
        public DbSet<MobileShop.Models.Manufacturer> Manufacturer { get; set; }
    }
}
