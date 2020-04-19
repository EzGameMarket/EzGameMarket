using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.API.Data
{
    public class AddressesDbContext : DbContext
    {
        public AddressesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
