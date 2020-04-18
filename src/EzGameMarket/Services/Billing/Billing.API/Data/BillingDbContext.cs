using Billing.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Data
{
    public class InvoicesDbContext : DbContext
    {
        public InvoicesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserInvoice> UserInvoices { get; set; }
        public DbSet<OwnCompanyDataModel> OwnData { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}
