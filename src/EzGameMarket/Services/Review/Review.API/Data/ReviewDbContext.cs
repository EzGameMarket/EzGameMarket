using Microsoft.EntityFrameworkCore;
using Review.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Data
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserReview> Reviews { get; set; }
    }
}