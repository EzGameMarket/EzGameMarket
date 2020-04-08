using CouponCode.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Data
{
    public class CouponCodeDbContext : DbContext
    {
        public CouponCodeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CouponCodeModel> CouponCodes { get; set; }
        public DbSet<DiscountModel> Discounts { get; set; }
    }
}
