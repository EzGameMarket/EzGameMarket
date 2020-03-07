using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime LastLogin { get; set; }
        public DateTime Registration { get; set; }
        public int MyProperty { get; set; }

        public List<Address> Addresses { get; set; }
        public List<CreditCard> Cards { get; set; }
        public List<PromoCode> Codes { get; set; }
    }
}
