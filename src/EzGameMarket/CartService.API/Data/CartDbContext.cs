using CartService.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Data
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}