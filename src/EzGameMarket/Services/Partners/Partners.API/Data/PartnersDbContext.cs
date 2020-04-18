using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.API.Data
{
    public class PartnersDbContext : DbContext
    {
        public PartnersDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
