using Microsoft.EntityFrameworkCore;
using PaymentService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.Data
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentDbContext( DbContextOptions options) : base(options)
        {
        }
    }
}
