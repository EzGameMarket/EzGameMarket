using Microsoft.EntityFrameworkCore;
using PaymentService.API.Models;

namespace PaymentService.API.Data
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}