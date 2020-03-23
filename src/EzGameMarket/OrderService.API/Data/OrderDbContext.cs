using Microsoft.EntityFrameworkCore;
using OrderService.API.Models.DbModels;

namespace OrderService.API.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders;
        public DbSet<OrderedItem> OrderedItems;
    }
}