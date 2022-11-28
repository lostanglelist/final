using logintest.Areas.Identity.Data;
using logintest.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHSHOP.Models
{
    public class ImageDbContext : DbContext
    {

        public ImageDbContext()
        {

        }


        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        {

        }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


    }
}
