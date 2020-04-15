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

        public DbSet<Invoice> Bills { get; set; }
        public DbSet<UserInvoice> UserBills { get; set; }
    }
}
