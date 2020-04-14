using Billing.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<UserBill> UserBills { get; set; }
    }
}
