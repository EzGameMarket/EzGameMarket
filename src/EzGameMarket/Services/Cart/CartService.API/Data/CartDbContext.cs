using CartService.API.Models;
using Microsoft.EntityFrameworkCore;

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