using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.PaymentMethods.API.Data
{
    public class UserPaymentMethodDbContext : DbContext
    {
        public UserPaymentMethodDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
