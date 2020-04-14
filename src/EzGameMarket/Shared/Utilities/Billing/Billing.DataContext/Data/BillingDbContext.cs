using Billing.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.DataContext.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillAction> Actions { get; set; }
    }
}
