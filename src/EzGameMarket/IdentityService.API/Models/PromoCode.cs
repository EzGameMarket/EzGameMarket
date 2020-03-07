using System;

namespace IdentityService.API.Models
{
    public class PromoCode
    {
        public string ID { get; set; }

        public string Code { get; set; }
        public double Discount { get; set; }
        public double ToOwner { get; set; }
        public bool Disabled { get; set; }
        public DateTime DisabledDate { get; set; }

        public string UserID { get; set; }
        public AppUser User { get; set; }
    }
}